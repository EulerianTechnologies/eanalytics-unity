using System;
using eulerian;

namespace eulerian
{
    public class Action
    {
        internal static readonly string KEY_REF = "ref";
        internal static readonly string KEY_IN = "in";
        private static readonly string KEY_OUT = "out";

        internal readonly JSONObject json = new JSONObject();

        public void SetReference(string reference)
        {
            json[KEY_REF] = reference;
        }

        public void SetIn(string inValue)
        {
            json[KEY_IN] = inValue;
        }

        public void AddOut(string[] outsValue)
        {
            JSONArray array = new JSONArray();
            foreach (string value in outsValue) {
                array.Add(value);
            }
            json[KEY_OUT] = array;
        }

    }
}
