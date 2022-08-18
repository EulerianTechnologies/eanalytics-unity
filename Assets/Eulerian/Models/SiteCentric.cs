namespace eulerian
{
    public class SiteCentric
    {
        internal readonly JSONObject json = new();

        public void Set(string key, string[] values)
        {
            JSONArray array = new();
            foreach (string value in values)
            {
                array.Add(value);
            }
            json[key] = array;
        }

    }
}
