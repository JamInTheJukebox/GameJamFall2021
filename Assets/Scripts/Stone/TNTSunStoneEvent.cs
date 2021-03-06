using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTSunStoneEvent : StoneInteractable
{
    public Gradient TNT_Gradient;
    public SpriteRenderer TNT_SpriteRenderer;
    private float MaxTime;
    private void Awake()
    {
        MaxTime = SecondsToInteract;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Tags.SUNSTONE)
        {
            InteractWithStone();
            if(MaxTime != 0)
            {
                if(TNT_SpriteRenderer == null) { return; }
                TNT_SpriteRenderer.color = TNT_Gradient.Evaluate(SecondsToInteract / MaxTime);
            }
        }
    }
}
