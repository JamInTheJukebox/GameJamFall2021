using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToLevel1()
    {
        GameManager.instance.ChangeScene("David");
    }

    private void Awake()
    {
        GameManager.instance.enablePausing(false);
    }
}