using System;
using UnityEngine;

namespace eulerian
{

    public class EAProperties
    {
        // Global params
        private static readonly string KEY_EOS = "eos";
        private static readonly string KEY_EHW = "ehw";
        private static readonly string KEY_EUIDL = "euidl";
        private static readonly string KEY_URL = "url";
        private static readonly string KEY_APPNAME = "ea-appname";
        private static readonly string KEY_EPOCH = "ereplay-time";
        private static readonly string KEY_APP_VERSION = "ea-appversion";
        private static readonly string KEY_ADID = "ea-unity-adid";

        // Page params
        private static readonly string KEY_PAGE_LATITUDE = "ea-lat";
        private static readonly string KEY_PAGE_LONGITUDE = "ea-lon";
        private static readonly string KEY_PAGE_PATH = "path";
        private static readonly string KEY_PAGE_EMAIL = "email";
        private static readonly string KEY_PAGE_UID = "uid";
        private static readonly string KEY_PAGE_PROFILE = "profile";
        private static readonly string KEY_PAGE_GROUP = "pagegroup";
        private static readonly string KEY_PAGE_ACTION = "action";
        private static readonly string KEY_PAGE_PROPERTY = "property";
        private static readonly string KEY_PAGE_NEW_CUSTOMER = "newcustomer";
        private static readonly string KEY_PAGE_CFLAG = "cflag";

        public static string ADID { get; internal set; }

        internal readonly JSONObject json = new JSONObject();

        public EAProperties(string path)
        {
            SetPath(path);
        }

        public JSONObject ToJSON()
        {
            AppendGlobalParams(json);
            return json;
        }

        private void AppendGlobalParams(JSONObject jsonObject)
        {
            jsonObject[KEY_EOS] = SystemInfo.operatingSystem;
            jsonObject[KEY_EHW] = SystemInfo.deviceModel;
            jsonObject[KEY_EUIDL] = SystemInfo.deviceUniqueIdentifier;
            jsonObject[KEY_URL] = "http://" + Application.identifier;
            jsonObject[KEY_APPNAME] = Application.productName;
            jsonObject[KEY_APP_VERSION] = Application.unityVersion;

            if (!string.IsNullOrEmpty(ADID))
            {
                jsonObject[KEY_ADID] = ADID;
            }

            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int cur_time = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
            jsonObject[KEY_EPOCH] = cur_time;
        }

        private void SetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Path must not be null or empty.");
                return;
            }
            if (!path[0].Equals("/"))
            {
                path = "/" + path;
            }
            json[KEY_PAGE_PATH] = path;
        }

        public void Set(string key, string value)
        {
            json[key] = value;
        }

        public void Set(string key, int value)
        {
            json[key] = value;
        }

        public void SetLocation(double latitude, double longitude)
        {
            json[KEY_PAGE_LATITUDE] = latitude.ToString();
            json[KEY_PAGE_LONGITUDE] = longitude.ToString();
        }

        public void SetNewCustomer(bool newCustomer)
        {
            json[KEY_PAGE_NEW_CUSTOMER] = newCustomer ? 1 : 0;
        }

        public void SetEmail(string email)
        {
            json[KEY_PAGE_EMAIL] = email;
        }

        public void SetUID(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                Debug.LogWarning("UID is empty, should not be.");
                return;
            }
            json[KEY_PAGE_UID] = uid;
        }

        public void SetProfile(string profile) => json[KEY_PAGE_PROFILE] = profile;

        public void SetPageGroup(string group) => json[KEY_PAGE_GROUP] = group;

        public void SetAction(Action action)
        {
            if (string.IsNullOrEmpty(action.json[Action.KEY_IN]) && string.IsNullOrEmpty(action.json[Action.KEY_REF]))
            {
                Debug.LogWarning("Action must have at least 'in' or 'ref' parameter set to be valid.");
            }
            json[KEY_PAGE_ACTION] = action.json;
        }

        public void SetProperty(SiteCentric property) => json[KEY_PAGE_PROPERTY] = property.json;

        public void SetCFlag(SiteCentric cFlag) => json[KEY_PAGE_CFLAG] = cFlag.json;
    }
}
