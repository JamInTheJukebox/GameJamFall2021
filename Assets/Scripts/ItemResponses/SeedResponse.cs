using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedResponse : GenericEvent
{
    public GameObject Plant;
    bool planted;

    public override bool Interact(Vector2 targetPos, Items selectedItem)        // did we successfully interact with the item?
    {
        if (base.Interact(targetPos) && RequiredItemToInteract == selectedItem)
        {
            return true; // failed to get the item
        }

        // failed to get the item   
        return false;
    }

    public override void executeInteractable()
    {
        // we'll probably use this to update the status of this gameobject in all 3 timelines.
        if(!planted)
            ChangeState();
    }
    public void ChangeState()       // make this timeline dependent
    {
        planted = true;
        Plant.SetActive(true);
    }
}
