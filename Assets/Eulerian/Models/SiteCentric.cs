using System;
using eulerian;

namespace eulerian
{
    public class SiteCentric
    {
        internal readonly JSONObject json = new JSONObject();

        public void Set(string key, string[] values)
        {
            JSONArray array = new JSONArray();
            foreach (string value in values)
            {
                array.Add(value);
            }
            json[key] = array;
        }

    }
}
