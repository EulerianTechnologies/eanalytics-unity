using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        }

    }

}
