using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;

namespace eulerian
{

    public class Eulerian : Singleton<Eulerian>
    {
        internal static readonly string SDK_VERSION = "1.1.0";

        private static readonly string KEY_SAVED_PAYLOAD = "unsync-eaprops";
        private string domain = "";

        // Prevent non-singleton constructor use.
        protected Eulerian() { }

        /// <summary>
        /// Initialize the Eulerian Unity SDK. 
        /// Consider using the Awake function from Unity's Monobehavior class as a starting place.
        /// </summary>
        /// <param name="domain">Eulerian Tracking Domain.</param>
        public static void Init(string domain)
        {
            Instance.domain = domain;
            if (domain.Contains(".eulerian.com"))
            {
                Debug.LogError("Domain cannot contain '.eulerian.com'.");
            }
            if (!Uri.IsWellFormedUriString("https://" + domain, UriKind.Absolute))
            {
                Debug.LogError("Domain is not well formed.");
            }
            if (string.IsNullOrEmpty(EAProperties.ADID))
            {
                Debug.Log("Request Advertising Identifier Async...");
                Application.RequestAdvertisingIdentifierAsync(
                    (string advertisingId, bool trackingEnabled, string error) =>
                    {
                        Debug.Log("advertisingId " + advertisingId + " " + trackingEnabled + " " + error);
                        EAProperties.ADID = advertisingId;
                    });
            }
            // Find EAProperties in storage
            var untracked = PlayerPrefs.GetString(KEY_SAVED_PAYLOAD, null);
            if (!string.IsNullOrEmpty(untracked))
            {
                PlayerPrefs.DeleteKey(KEY_SAVED_PAYLOAD); // untracked props will be saved (again) if upload failed.
                PlayerPrefs.Save();
                JSONArray json = (JSONArray)JSONNode.Parse(untracked);
                Debug.Log(json.Count + " EAProperties found in storage. Will try to sync.");
                Instance.PostData(json);
            }
        }

        public static string GetEuidl()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }

        public static bool IsInitialized()
        {
            if (string.IsNullOrEmpty(Instance.domain))
            {
                Debug.LogError("Eulerian Tracking Domain is not set. You must call Eulerian.Init().");
                return false;
            }
            return true;
        }

        public static void Track(EAProperties properties)
        {
            if (!IsInitialized()) return;
            if (properties == null) Debug.Log("Cannot track null properties.");
            Debug.Log("Tracking " + properties + " @domain: " + Instance.domain);
            Instance.PostData(properties);
        }

        public void PostData(EAProperties properties)
        {
            JSONArray json = new();
            json.Add(properties.ToJSON());
            StartCoroutine(Upload(json));
        }

        public void PostData(JSONArray json)
        {
            StartCoroutine(Upload(json));
        }

        IEnumerator Upload(JSONArray data)
        {
            DateTime epochStart = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int now = (int)(DateTime.UtcNow - epochStart).TotalSeconds;

            var jsonString = data.ToString();
            var url = "https://" + Instance.domain + "/collectorjson-unity/-/" + now;
            Debug.Log("Eulerian Analytics POST:\n- URL: " + url + "\n- data: " + jsonString);

            var www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);

            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);

            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Data upload failed with error: " + www.error);
                Save(data);
            }
            else
            {
                Debug.Log("Data upload complete with Status Code: " + www.responseCode);
            }
        }

        private void Save(JSONArray data)
        {
            var stored = PlayerPrefs.GetString(KEY_SAVED_PAYLOAD, null);
            JSONArray storageJson;
            if (string.IsNullOrEmpty(stored))
            {
                Debug.Log("Failed to send EAProperties. Will retry later.");
                storageJson = data;
            }
            else
            {
                storageJson = (JSONArray)JSONNode.Parse(stored);
                Debug.Log("Failed to send EAProperties (with " + storageJson.Count + " others EAProperties). Will retry later.");
                foreach (var item in data.Values.GetEnumerator())
                {
                    storageJson.Add(item);
                }
            }
            PlayerPrefs.SetString(KEY_SAVED_PAYLOAD, storageJson.ToString());
            PlayerPrefs.Save();
        }
    }

}