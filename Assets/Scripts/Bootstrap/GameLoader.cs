using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private TMP_Text _loadingText;

    private void Start()
    {
        string nextSceneName = GetNextSceneName();

        StartCoroutine(LoadNextSceneAsync(nextSceneName));
    }

    private IEnumerator LoadNextSceneAsync(string nextSceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextSceneName);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            _loadingSlider.value = progress;

            int progressPercentage = Mathf.RoundToInt(progress * 100);
            _loadingText.text = "Loading... " + progressPercentage + "%";

            yield return null;
        }

        yield return StartCoroutine(WaitForSceneInitialization(nextSceneName));
    }

    private IEnumerator WaitForSceneInitialization(string sceneName)
    {
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);

        while (!SceneIsInitialized(loadedScene))
        {
            yield return null;
        }
    }

    private bool SceneIsInitialized(Scene scene)
    {
        foreach (GameObject rootObject in scene.GetRootGameObjects())
        {
            if (!rootObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    private string GetNextSceneName()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("No next scene found. Returning to the current scene.");
            return SceneManager.GetActiveScene().name;
        }

        string nextSceneName = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
        nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextSceneName);

        return nextSceneName;
    }
}
