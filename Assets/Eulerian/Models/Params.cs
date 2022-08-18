namespace eulerian
{
    public class Params
    {
        internal readonly JSONObject json = new();

        public void AddParam(string key, string value) => json[key] = value;
        public void AddParam(string key, int value) => json[key] = value;
    }
}
