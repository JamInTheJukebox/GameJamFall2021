using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericEvent : Interactable
{

    [SerializeField] protected Items RequiredItemToInteract;
    public UnityEvent interactAction;           // specify what kind of information we are passing in with this action

    public override bool Interact(Vector2 targetPos, Items selectedItem)        // did we successfully interact with the item?
    {
        if (base.Interact(targetPos) && RequiredItemToInteract == selectedItem)
        {
            print("Planting Stuff");
            return true; // failed to get the item
        }

        // failed to get the item   
        return false;
    }

    public override void executeInteractable()
    {
        print("Watch it grow!!"); 
    }
}
