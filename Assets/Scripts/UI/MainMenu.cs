using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip ButtonClickSFX;
    public AudioClip QuitButtonSFX;
    public AudioClip Song;
    [Range(0, 1)] public int mymom;
    private void Awake()
    {
        if(AudioManager.Instance != null)
            AudioManager.Instance.PlayMusicwithCrossFade(Song);
        mymom = 9;
    }
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
