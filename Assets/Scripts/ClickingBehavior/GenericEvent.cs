using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericEvent : Interactable
{

    [SerializeField] protected Items RequiredItemToInteract;

    public override bool Interact(Vector2 targetPos, Items selectedItem)        // did we successfully interact with the item?
    {
        if (base.Interact(targetPos) && (RequiredItemToInteract == selectedItem))
        {
            return true; // failed to get the item
        }

        // failed to get the item   
        return false;
    }

    public override bool Interact(Vector2 targetPos)
    {
        bool requireItem = RequiredItemToInteract != Items.None;
        if (requireItem && !MasterUserInterface.instance.ItemUIController.IsUsingItem())
            return false;

        return base.Interact(targetPos);
    }

    public override void executeInteractable()
    {
        onInteract.Invoke();
    }

    public virtual int executeInteractable(ItemScriptable item)
    {
        onInteract.Invoke();
        return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TouchToInteract) { return; }

        if (collision.gameObject.tag == Tags.PLAYER)
        {
            executeInteractable();
        }
    }
}
