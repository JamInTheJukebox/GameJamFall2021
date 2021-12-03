using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoneInteractable : MonoBehaviour
{
    [SerializeField] protected float SecondsToInteract = 3;      // seconds you have to mouse on this item in order to trigger the interactable.
    public UnityEvent StoneEvent;
    public bool ResetToOriginal;
    private float originalSeconds;

    private void Awake()
    {
        originalSeconds = SecondsToInteract;
    }

    public virtual void InteractWithStone()
    {
        SecondsToInteract -= Time.deltaTime;
        if (SecondsToInteract < 0)
        {
            StoneEvent?.Invoke();
            if (ResetToOriginal)
            {
                SecondsToInteract = originalSeconds;

            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Tags.SUNSTONE && this.enabled)
        {
            InteractWithStone();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
