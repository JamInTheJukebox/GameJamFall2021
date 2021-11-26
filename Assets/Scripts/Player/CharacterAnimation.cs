using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterAnimation : MonoBehaviour
{
    private string m_State;
    private string time;

    public string State
    {
        get { return m_State; }
        set
        {
            if (m_State != value)
            {
                m_State = value;
                charAnimator.Play(m_State);
            }
        }
    }
    Rigidbody2D rb;
    bool running;

    public float runMarginOfError = 0.001f;
    private Animator charAnimator;
    private TimelineController timeline;
    private SortingOrderScript sortorder;
    public SortingOrderScript sfxSordingOrderScript;
    private void Awake()
    {
        charAnimator = GetComponent<Animator>();
        timeline = FindObjectOfType<TimelineController>();
        rb = GetComponent<Rigidbody2D>();
        sortorder = GetComponent<SortingOrderScript>();
        if (sfxSordingOrderScript == null)
            sfxSordingOrderScript = GetComponentInChildren<SortingOrderScript>();
    }

    // Update is called once per frame
    void Update()
    {
        setAnimation();
    }

    private void setAnimation()
    {
        string tempVari;
        if (timeline == null) { return; }
        if (timeline.currentTime == Timeline.Past)
            time = "Past";
        else if (timeline.currentTime == Timeline.Present)
            time = "Present";
        else if (timeline.currentTime == Timeline.Future)
            time = "Future";
        // If changing time
        if (timeline.isChangingTime)
        {
            tempVari = AnimationTags.PLAYER_WAND;
        }
        // If Running
        else if (running)
        {
            tempVari = AnimationTags.PLAYER_RUN;
        }
        // If Standing still
        else
        {
            tempVari = AnimationTags.PLAYER_IDLE;
        }
        State = tempVari + time;
    }

    public void SetChangingTimeLineLayer()
    {
        sfxSordingOrderScript.SetSortingLayer("TimelineTransition");
        sortorder.SetSortingLayer("TimelineTransition");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        running = Mathf.Abs(rb.velocity.x) > runMarginOfError;
    }
}
    // add method to call change sorting order on both the player and the sfx