using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoneInteractable : MonoBehaviour
{
    [SerializeField] protected float SecondsToInteract = 3;      // seconds you have to mouse on this item in order to trigger the interactable.
    public UnityEvent StoneEvent;

    public virtual void InteractWithStone()
    {
        SecondsToInteract -= Time.deltaTime;
        if (SecondsToInteract < 0)
        {
            StoneEvent?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Tags.SUNSTONE)
        {
            InteractWithStone();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
