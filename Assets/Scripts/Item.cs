using UnityEngine;

public class Item : Interactable
{
    // item Scriptable Object here.
    public override bool Interact(Vector2 targetPos)
    {
        if (base.Interact(targetPos))
        {
            return false; // failed to get the item
        }
   
        // get the item
        return true;
    }
}
