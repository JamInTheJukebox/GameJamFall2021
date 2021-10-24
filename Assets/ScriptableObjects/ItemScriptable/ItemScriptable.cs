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

    public int itemUses = 0;

    public virtual void UseItem(Interactable newInteractable)       // use the item!
    {
        newInteractable.executeInteractable();
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

    public void init(ItemScriptable SO)      // used for creating instances of objects
    {
        cursorImage = SO.cursorImage;
        itemSprite = SO.itemSprite;
        itemType = SO.itemType;
        itemAvailable = SO.itemAvailable;
        itemUses = SO.itemUses;
        requiresInteractable = SO.requiresInteractable;
    }
}