namespace Base.Data.Scenes
{
    public abstract class SceneData
    {
        public SceneData(string sceneName, string uIName)
        {
            SceneName = sceneName;
            UIName = uIName;
        }

        public string SceneName { get; }
        public string UIName { get; }
    }
}
