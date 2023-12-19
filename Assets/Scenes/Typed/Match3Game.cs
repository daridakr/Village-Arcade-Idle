namespace IJunior.TypedScenes
{
    using UnityEngine.SceneManagement;

    public class Match3Game : TypedScene
    {
        private const string _sceneName = "Match3Game";

        public static void Load(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            LoadScene(_sceneName, loadSceneMode);
        }

        public static UnityEngine.AsyncOperation LoadAsync(LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            return LoadScene(_sceneName, loadSceneMode);
        }
    }
}