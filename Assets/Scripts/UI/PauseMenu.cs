using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool m_pauseEnabled = false;
    public bool pauseEnabled
    {
        get { return m_pauseEnabled; }
        set
        {
            if(value != m_pauseEnabled)
            {
                m_pauseEnabled = value;
                gameObject.SetActive(pauseEnabled);
            }
        }
    }

    private Animator PauseMenuAnim;
    private bool canPauseAgain = true;

    int m_pauseState = 0;
    public int pauseState   // 0 -> 1 = pause, 1 -> 0, unpause
    {
        get { return m_pauseState; }
        set
        {
            if (value != m_pauseState)
            {
                m_pauseState = value;
                if (m_pauseState > 1)
                {
                    m_pauseState = 0;
                }
                else if (m_pauseState < 0)
                {
                    m_pauseState = 0;
                }
                PauseMenuAnim.SetInteger("PausedState", m_pauseState);
            }
        }
    }
    private void Awake()
    {
        PauseMenuAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (pauseEnabled && canPauseAgain)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                pause();
            }
        }
    }
    
    public void pause()
    {
        Time.timeScale = pauseState;
        pauseState++;
        canPauseAgain = false;
        Invoke("canPauseAgain",0.2f);
    }
    public void setCanPauseAgain()
    {
        canPauseAgain = true;
    }

    public void enablePausing(bool newState)
    {
        pauseEnabled = newState;
    }
    
    public void QuitToMenu()
    {
        pauseState = 0;
        PauseMenuAnim.Play("PauseDefaultState");
        enablePausing(false);
        GameManager.instance.ChangeScene(SceneNames.MAIN_MENU);         // change to level select in the future.
    }

    public void RestartLevel()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        pauseState = 0;
        enablePausing(false);
        GameManager.instance.ChangeScene(SceneName);         // change to level select in the future.
    }
    public bool isPaused()
    {
        return pauseState == 1;
    }
}
