namespace eulerian
{
    public class EASearch : EAProperties
    {
        private static readonly string KEY_NAME = "name";
        private static readonly string KEY_RESULTS = "results";
        private static readonly string KEY_PARAMS = "params";
        private static readonly string KEY_SEARCH_ENGINE = "isearchengine";

        internal readonly JSONObject jsonEngine = new JSONObject();

        public EASearch(string path, string name) : base(path)
        {
            jsonEngine[KEY_NAME] = name;
            Update();
        }

        public void SetResults(int results)
        {
            jsonEngine[KEY_RESULTS] = results;
            Update();
        }

        public void SetParams(Params parameters)
        {
            jsonEngine[KEY_PARAMS] = parameters.json;
            Update();
        }

        private void Update()
        {
            json[KEY_SEARCH_ENGINE] = jsonEngine;
        }

    }
}
