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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        loadFadeIn = Animator.StringToHash(AnimationTags.LOAD_FADEIN);
        loadFadeOut = Animator.StringToHash(AnimationTags.LOAD_FADEOUT);
        ChangeScene(SceneNames.MAIN_MENU);
    }

    public void ChangeScene(string newScene)
    {
        StartCoroutine(loadLevelAsynchronously(newScene));
    }
    IEnumerator loadLevelAsynchronously(string newScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        if (currentScene != "")
        {
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
    }

}
