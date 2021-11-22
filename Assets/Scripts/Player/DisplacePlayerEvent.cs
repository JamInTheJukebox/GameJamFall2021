using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacePlayerEvent : GenericEvent
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    Transform player;

    public override bool Interact(Vector2 targetPos)        // did we successfully interact with the item?
    {
        print("lol3");

        if (player != null)
        {
            return true; // failed to get the item
        }
        print("lol2");

        // failed to get the item   
        return false;
    }

    public override void executeInteractable()
    {
        print("lol");
        onInteract.Invoke();
        if(Vector2.Distance(player.position,pointA.position) < Vector2.Distance(player.position, pointB.position))
        {
            player.position = pointB.position;
        }
        else
        {
            player.position = pointA.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYER)
        {
            player = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYER)
        {
            player = null;
        }
    }

}
