using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Transition : MonoBehaviour
{
    public bool CanTransitionLevel = true;          // require level transitions.
    public bool TouchToChangeLevel;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            //GameManager.StartInMainMenu = false;
            //GameManager.currentLvl = SceneManager.GetActiveScene().name;
            //SceneManager.LoadScene(SceneNames.GAME_MANAGER);
        }
    }
    public void GoToNextLevel()
    {
        if (!CanTransitionLevel) { return; }        // add a dialogue box here.
        try
        {
            int i;
            string SceneName = GameManager.GetCurrentLevelName();
            SceneName = SceneName.Replace("Level_", "");
            i = int.Parse(SceneName);
            i++;
            SceneName = "Level_" + i;
            GameManager.instance.ChangeScene(SceneName);
        }
        catch
        {
            Debug.LogWarning("Level_Transition GoToNextLevel failed");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(TouchToChangeLevel)
            GoToNextLevel();
    }

    public void WinConditionComplete()
    {
        CanTransitionLevel = true;
    }

    public void Die()
    {
        // play effect here then restart the level
        Invoke("restartLVL",0.1f);
    }

    public void restartLVL() {
        MasterUserInterface.instance.PauseMenu.RestartLevel();
    }
}