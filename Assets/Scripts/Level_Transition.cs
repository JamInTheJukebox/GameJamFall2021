using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Transition : MonoBehaviour
{

    public void GoToNextLevel()
    {
        int i;
        string SceneName = SceneManager.GetActiveScene().name;
        SceneName = SceneName.Replace("Level_", "");
        i = int.Parse(SceneName);
        i++;
        SceneName = "Level_" + i;
        GameManager.instance.ChangeScene(SceneName);
    }
}