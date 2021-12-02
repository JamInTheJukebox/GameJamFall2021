using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] VideoPlayer currentVideo;
    [SerializeField] CanvasGroup SkipButton;
    public float TimeToShowSkipButton;
    public float SkipButtonOnStandbyTime;
    private float currentStandByTimer;
    private bool TryingToSkip = false;
    //Play the video!
    private void Awake()
    {
        //Invoke repeating of checkOver method
        currentVideo.loopPointReached += ChangeScene;
        currentVideo.Play();
    }

    private void Update()
    {
        if (GetSkipInput())
        {
            TryingToSkip = true;
        }
        if (TryingToSkip)
        {
            currentStandByTimer += Time.deltaTime;
            if(currentStandByTimer < TimeToShowSkipButton)
            {
                SkipButton.alpha = Mathf.Lerp(0, 1, currentStandByTimer / TimeToShowSkipButton);
            }
            else if(currentStandByTimer > TimeToShowSkipButton && currentStandByTimer < SkipButtonOnStandbyTime)
            {
                SkipButton.alpha = 1;
                // skip button showing.
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeScene(null);
                }
            }
            else if( currentStandByTimer >= SkipButtonOnStandbyTime)
            {
                SkipButton.alpha = Mathf.Lerp(1, 0, (currentStandByTimer- SkipButtonOnStandbyTime) / (TimeToShowSkipButton));
                if (GetSkipInput())
                {
                    currentStandByTimer = TimeToShowSkipButton;
                }
                if(SkipButton.alpha == 0)
                {
                    currentStandByTimer = 0;
                    TryingToSkip = false;
                }
            }
        }
        else
        {
            SkipButton.alpha = 0;
        }

    }

    private void ChangeScene(VideoPlayer vp)
    {
        if(vp != null)
            vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        SkipButton.alpha = 0;
        GameManager.instance.ChangeScene(SceneNames.MAIN_MENU);

    }

    private bool GetSkipInput()
    {
        return Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2));
    }


}
