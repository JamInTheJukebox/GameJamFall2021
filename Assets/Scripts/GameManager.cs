using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Static Instance
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            return m_instance;
        }
        private set
        {
            m_instance = value;
        }
    }
    #endregion
    private static string currentScene = "";
    // loading bar
    [SerializeField] Slider loadingBar;
    [SerializeField] Animator loadingAnimation;
    int loadFadeIn; int loadFadeOut;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        loadFadeIn = Animator.StringToHash(AnimationTags.LOAD_FADEIN);
        loadFadeOut = Animator.StringToHash(AnimationTags.LOAD_FADEOUT);
        ChangeScene(currentScene);
    }

    public void ChangeScene(string newScene)
    {
        if(CheckSceneValidation(newScene) < 0) { Debug.LogWarning("GameManager.cs: Scene not found in build settings");
            currentScene = "";
            StartCoroutine(loadLevelAsynchronously(SceneNames.MAIN_MENU));
            return;
        }
        StartCoroutine(loadLevelAsynchronously(newScene));
    }
    private int CheckSceneValidation(string sceneName)
    {
        var checkIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/" + sceneName);
        checkIndex = (checkIndex < 0) ? SceneUtility.GetBuildIndexByScenePath("Scenes/Levels/" + sceneName) : checkIndex;
        return checkIndex;

    }
    IEnumerator loadLevelAsynchronously(string newScene)
    {
        if (SceneManager.GetSceneByName(currentScene).isLoaded)
        {
            AsyncOperation operation2 = SceneManager.UnloadSceneAsync(currentScene);
            while (!operation2.isDone)
            {
                // unloading scene;
                yield return null;
            }
            print("done unloading level");
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        loadingAnimation.Play(loadFadeIn);
        currentScene = newScene;

        loadingBar.gameObject.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }
        loadingAnimation.Play(loadFadeOut);
        loadingBar.gameObject.SetActive(false);
        loadingBar.value = 0;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        // if the active scene is not game manager on startup, load it and load the scene we were just on.
        if(SceneManager.GetActiveScene().name != SceneNames.GAME_MANAGER)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(SceneNames.GAME_MANAGER);
            currentScene = scene.name;
        }

        Debug.Log("First Scene loaded!");
    }

    public static string GetCurrentLevelName()
    {
        return currentScene;
    }

}
