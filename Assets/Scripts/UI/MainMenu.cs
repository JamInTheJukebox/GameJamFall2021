using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip ButtonClickSFX;
    public AudioClip QuitButtonSFX;

    public void PlayButtonSFX()
    {
        AudioManager.Instance.PlaySFX(ButtonClickSFX);
    }
    public void PlayBackSFX()
    {
        AudioManager.Instance.PlaySFX(QuitButtonSFX);
    }

    public void QuitGame()
    {
        PlayBackSFX();
        Application.Quit();
    }
}
