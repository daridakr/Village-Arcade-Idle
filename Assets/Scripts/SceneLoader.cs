using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneLoader : MonoBehaviour
{
    private const string _loadingSceneName = "Loading";

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoadLoadingScene = SceneManager.LoadSceneAsync(_loadingSceneName);

        while (!asyncLoadLoadingScene.isDone)
        {
            yield return null;
        }

        Scene currentActiveScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentActiveScene);

        AsyncOperation asyncLoadTargetScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoadTargetScene.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(_loadingSceneName);
    }
}
