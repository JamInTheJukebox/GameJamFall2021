using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUIController : MonoBehaviour
{
    // collect an item by storing it in this list.
    [SerializeField] List<ItemScriptable> inventoryItems;
    #region Properties
    ItemScriptable m_equippedItem;
    public ItemScriptable equippedItem
    {
        get { return m_equippedItem; }
        set
        {
            if(value != m_equippedItem)
            {
                // subscribe new event here to "interact event"
                m_equippedItem = value;
                UpdateItemSprite();
                UpdateCursor();
                UpdateCounter();
            }
        }
    }

    int m_inventoryIndex = 0;
    public int inventoryIndex
    {
        get { return m_inventoryIndex; }
        set
        {
            if(value != m_inventoryIndex)
            {
                if(value >= inventoryItems.Count) { m_inventoryIndex = 0; }
                else if(value < 0) { m_inventoryIndex = inventoryItems.Count - 1; }
                else { m_inventoryIndex = value; }
                m_inventoryIndex = Mathf.Clamp(m_inventoryIndex, 0, inventoryItems.Count);
                if(inventoryItems.Count == 0)   // inventory is empty
                {
                    ItemPNG.sprite = defaultItemPanelSprite;
                    TextDisplayParent.SetActive(false);
                    usingItem = false;
                    return;
                }
                equippedItem = inventoryItems[m_inventoryIndex];
            }
        }
    }

    private bool m_usingItem;
    public bool usingItem
    {
        get { return m_usingItem; }
        set
        {
            if (value != m_usingItem)
            {
                m_usingItem = value;
                UpdateItemColors();
                UpdateCursor();
            }
        }
    }

    #endregion

    [Header("Panels")]
    [SerializeField] Image ItemPNG;
    [SerializeField] Sprite defaultItemPanelSprite;
    [SerializeField] Button EnableItemButton;
    [SerializeField] TextMeshProUGUI TextDisplay;
    [SerializeField] GameObject TextDisplayParent;
    [SerializeField] Color ItemActiveColor;

    public delegate void interactEvent();

    private void Awake()
    {
        if (inventoryItems.Count != 0)
            equippedItem = inventoryItems[0];
    }

    private void OnEnable()
    {
        if (equippedItem == null)
            TextDisplayParent?.SetActive(false);        // disable the text Display
    }

    public void CollectItem(ItemScriptable newItem)
    {
        ItemScriptable item = inventoryItems.SingleOrDefault(x => x.itemType == newItem.itemType);
        if(!item)     // we do not want the same item in the inventory!
        {
            var itemToAdd = createItemInstance(newItem);
            inventoryItems.Add(itemToAdd);
            equippedItem = itemToAdd;
            inventoryIndex = inventoryItems.Count - 1;
            usingItem = false;
        }
        else if(item && item.useCounter)
        {
            item.itemUses += newItem.itemUses;
            UpdateCounter();
            // increment the item if it is a counter.
            
        }
    }

    ItemScriptable createItemInstance(ItemScriptable original)     // create instances of scriptable objects to avoid altering the values observed in the project files.
    {
        switch (original.itemType)
        {
            case Items.waterCan:
            {
                var copy = ScriptableObject.CreateInstance<Waterbucket>();
                copy.init((Waterbucket)original);
                return copy;
            }
            default:
            {
                var copy = ScriptableObject.CreateInstance<ItemScriptable>();
                    copy.init(original);
                return copy;
            }
        }
    }

    public void UseItem()
    {
        equippedItem.UseItem();
        if (equippedItem.ItemDepleted())
        {
            RemoveItem();
            return;
        }

        UpdateCounter();
        UpdateCursor();
        UpdateItemSprite();
        UpdateItemColors();
    }

    public void UseItem(Interactable ObjectToInteractWith)
    {
        // do a check if we can use the item here.
        equippedItem.UseItem(ObjectToInteractWith);
        print("object to interact with " + ObjectToInteractWith);
        if(equippedItem.ItemDepleted())
        {
            RemoveItem();
            return;
        }
        UpdateCounter();
        UpdateCursor();
        UpdateItemSprite();
        UpdateItemColors();
    }

    public void ToggleItem()            // equip and unequip an item.
    {
        if(equippedItem == null) { return; }
        if (MasterUserInterface.instance.StoneController.isUsingStone)
            MasterUserInterface.instance.StoneController.DisableStone();
        usingItem = !usingItem;
        // add timer right here
    }

    public void ChangeItem(int delta)
    {
        inventoryIndex += delta;
    }

    public void RemoveItem()
    {
        // remove an item that has been depleted.
        inventoryItems.Remove(equippedItem);
        Destroy(equippedItem);
        usingItem = false;
        inventoryIndex--;
    }

    public bool IsUsingItem()
    {
        return usingItem;
    }

    public void DisableItem()
    {
        usingItem = false;
    }

    public Items GetItemTypeInUse()
    {
        if (equippedItem == null || !IsUsingItem())         // if we have no items or we aren't using an item, return none.
            return Items.None;
        return equippedItem.itemType;
    }

    #region UpdateFunctions

    private void UpdateItemSprite()
    {
        ItemPNG.sprite = equippedItem.itemSprite;
    }

    private void UpdateCounter()
    {
        TextDisplayParent.SetActive(equippedItem.itemUses != 0);
        print(equippedItem.itemUses.ToString());
        TextDisplay.text = equippedItem.itemUses.ToString();
    }

    private void UpdateCursor()
    {
        if (usingItem)
        {
            if (equippedItem.cursorImage != null)
            {
                Cursor.SetCursor(equippedItem.cursorImage, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                // make it glow?
            }
        }
        else
        {
            // reset to default cursor;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void UpdateItemColors()         // makes the circle around the item either yellow(Indicates item is in use) or white(Item not it use)
    {
        var colors = EnableItemButton.colors;
        colors.normalColor = (usingItem) ? ItemActiveColor : Color.white;
        EnableItemButton.colors = colors;
    }
    #endregion
}
