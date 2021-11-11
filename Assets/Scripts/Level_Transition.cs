using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Transition : MonoBehaviour
{

    public void GoToNextLevel()
    {
        try
        {
            int i;
            string SceneName = SceneManager.GetActiveScene().name;
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
        GoToNextLevel();
    }
}