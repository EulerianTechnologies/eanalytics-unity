using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;

namespace eulerian
{

    public class Eulerian : Singleton<Eulerian>
    {
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

        public static void Track(string eventName)
        {
            if (!IsInitialized()) return;
            Debug.Log("Let's track " + eventName + " @domain: " + Instance.domain);
            Instance.PostData(eventName);
        }

        public void PostData(string eventName)
        {
            StartCoroutine(Upload(eventName));
        }

        IEnumerator Upload(string eventName)
        {
            JSONObject json = new JSONObject();
            json["event"] = eventName;

            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int now = (int)(DateTime.UtcNow - epochStart).TotalSeconds;

            var jsonString = json.ToString();
            var url = "https://" + Instance.domain + "/collectorjson-unity/-/" + now;
            Debug.Log("Eulerian Analytics POST:\n- URL: " + url + "\n- data: " + jsonString);

            var www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);

            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);

            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Data upload complete with Status Code: " + www.responseCode);
            }
        }

    }

}
