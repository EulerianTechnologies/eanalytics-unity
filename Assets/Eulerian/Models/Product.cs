namespace eulerian
{
    public class Product
    {
        private static readonly string KEY_REF = "ref";
        private static readonly string KEY_NAME = "name";
        private static readonly string KEY_PARAMS = "params";
        private static readonly string KEY_GROUP = "group";

        internal readonly JSONObject json = new();

        public Product(string reference)
        {
            json[KEY_REF] = reference;
        }

        public void SetName(string name) => json[KEY_NAME] = name;
        public void SetGroup(string group) => json[KEY_GROUP] = group;
        public void SetParams(Params parameters) => json[KEY_PARAMS] = parameters.json;
    }
}
