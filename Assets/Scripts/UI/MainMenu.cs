using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if(GameManager.instance == null)
        {
            SceneManager.LoadScene(SceneNames.GAME_MANAGER);
        }
    }

    public void GoToLevel1()
    {
        GameManager.instance.ChangeScene("David");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
