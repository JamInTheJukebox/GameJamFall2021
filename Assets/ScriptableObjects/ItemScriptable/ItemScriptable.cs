using UnityEngine;

[CreateAssetMenu(fileName = "ItemInformation", menuName = "ScriptableObjects/ItemScriptable/GenericItem", order = 1)]
public class ItemScriptable : ScriptableObject
{
    public Items itemType;

    public bool itemAvailable = true;

    [Tooltip("Do you need to touch another interactable in order to use this item?")]
    public bool requiresInteractable = true;

    public Sprite itemSprite;

    public Texture2D cursorImage;

    [Header("Limited Use Settings")]
    public int itemUses = 0;
    public bool useCounter = false;

    public virtual void UseItem(Interactable newInteractable)       // use the item!
    {
        var interactable = (GenericEvent)newInteractable;
        interactable.executeInteractable(this);  // if we successfully used the item, decrement. Make the executeInteractable return int/Bool instead of void in the future.
        if (itemUses > 0)
            itemUses--;
    }

    public virtual void UseItem()       // use the item!
    {
        if (itemUses > 0)
            itemUses--;
    }
    public virtual void SetCursorImage() { }

    public virtual void SetItemImage() { }

    public virtual bool CanUseItem() {
        return true;
    }   // add custom cooldowns to items with counters.

    public virtual bool KeepItem()       // conditions to destroy the item we are using.
    {
        return itemUses > 0 || !useCounter;
    }
    public void init(ItemScriptable SO)      // used for creating instances of objects
    {
        cursorImage = SO.cursorImage;
        itemSprite = SO.itemSprite;
        itemType = SO.itemType;
        itemAvailable = SO.itemAvailable;
        itemUses = SO.itemUses;
        requiresInteractable = SO.requiresInteractable;
        useCounter = SO.useCounter;
    }
}