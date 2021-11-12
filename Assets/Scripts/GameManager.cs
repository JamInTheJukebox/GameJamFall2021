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
    private string currentScene = "";
    // loading bar
    [SerializeField] Slider loadingBar;
    [SerializeField] Animator loadingAnimation;
    int loadFadeIn; int loadFadeOut;
    public static bool StartInMainMenu = true;
    public static string currentLvl;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        loadFadeIn = Animator.StringToHash(AnimationTags.LOAD_FADEIN);
        loadFadeOut = Animator.StringToHash(AnimationTags.LOAD_FADEOUT);
        if (StartInMainMenu)
        {
            ChangeScene(SceneNames.MAIN_MENU);
        }
        else
        {
            ChangeScene(currentLvl);
        }
    }

    public void ChangeScene(string newScene)
    {
        if(CheckSceneValidation(newScene) < 0) { Debug.LogError("GameManager.cs: Scene not found in build settings"); return; }
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
        AsyncOperation operation = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        
        if (currentScene != "")
        {
            if(newScene == currentScene)
                SceneManager.UnloadScene(currentScene);
            else
                SceneManager.UnloadSceneAsync(currentScene);
        }
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
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));

    }

}
