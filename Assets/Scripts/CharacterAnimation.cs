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
            if(m_State != value)
            {
                m_State = value;
                charAnimator.Play(m_State);
            }
        }
    }
    Rigidbody2D rb;
    public float runMarginOfError = 0.001f;
    private Animator charAnimator;
    private TimelineController timeline;
    
    private void Awake()
    {
        charAnimator = GetComponent<Animator>();
        timeline = FindObjectOfType<TimelineController>();
        rb = GetComponent<Rigidbody2D>();
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

        // If Running
        if (Mathf.Abs(rb.velocity.x) > runMarginOfError)
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
}