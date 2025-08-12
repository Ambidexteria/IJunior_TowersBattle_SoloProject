namespace Base.Data.Scenes
{
    public abstract class SceneData
    {
        public SceneData(string sceneName)
        {
            SceneName = sceneName;
        }

        public string SceneName { get; }
    }
}
