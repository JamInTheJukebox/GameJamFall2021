using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_AnimationTimeline : MonoBehaviour
{

    private TimelineController timelineController;      // in the future, this code will be located in a animator script for the player.
    private Animator transitionAnim;

    private void Awake()
    {
        timelineController = FindObjectOfType<TimelineController>();
        transitionAnim = GetComponent<Animator>();
    }

    public void StartTransition()
    {
        transitionAnim.Play("tempPlayerAnim");
    }

    public void ChangeEnvironment()
    {

    }

    public void FadeBackground()
    {

    }

    public void UnfadeBackground()
    {

    }
}
