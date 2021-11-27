using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : GenericEvent
{
    public GameObject Water;
    public bool InfiniteWaterSource = false;    
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
    }
    public override int executeInteractable(ItemScriptable item)
    {
        Waterbucket bucket = item as Waterbucket;
        if(bucket == null) { return 0; }
        if (InfiniteWaterSource)
            return 0;    
        ChangeFilledState(bucket.isFilled());
        int newState = ChangeFilledState(bucket.isFilled()) ? 1 : 0;
        return newState;
    }

    public bool ChangeFilledState(bool fullWaterBucket)     // true = successful change in state. false = unsuccessful change in state.
    {
        if(Water.activeSelf == fullWaterBucket) // if both are empty or both are full, do nothing
        {
            return Water.activeSelf;
        }
        Water.SetActive(fullWaterBucket);  // transfer water
        return Water.activeSelf;
    }
}
