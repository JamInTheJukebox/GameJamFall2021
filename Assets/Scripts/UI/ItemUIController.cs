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
                ItemPNG.sprite = m_equippedItem.itemSprite;
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
    [SerializeField] Button EnableItemButton;
    [SerializeField] TextMeshProUGUI TextDisplay;
    [SerializeField] GameObject TextDisplayParent;
    [SerializeField] Color ItemActiveColor;

    public delegate void interactEvent();

    private void OnEnable()
    {
        if (equippedItem == null)
            TextDisplayParent?.SetActive(false);        // disable the text Display
    }
    public void collectItem(ItemScriptable newItem)
    {
        if(!inventoryItems.Any(x => x.itemType == newItem.itemType))     // we do not want the same item in the inventory!
        {
            inventoryItems.Add(newItem);
            equippedItem = newItem;
            inventoryIndex = inventoryItems.Count - 1;
            usingItem = false;
        }
    }

    public void toggleItem()            // equip and unequip an item.
    {
        if(equippedItem == null) { return; }
        usingItem = !usingItem;
        // add timer right here
    }

    public void ChangeItem(int delta)
    {
        inventoryIndex += delta;
    }

    public void removeItem()
    {
        // remove an item that has been depleted.
    }

    public bool isUsingItem()
    {
        return usingItem;
    }

    #region UpdateFunctions
    private void UpdateCounter()
    {
        TextDisplayParent.SetActive(equippedItem.itemUses != 0);
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
                // make it glow;
            }
        }
        else
        {
            // reset to default cursor;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void UpdateItemColors()
    {
        var colors = EnableItemButton.colors;
        colors.normalColor = (usingItem) ? ItemActiveColor : Color.white;
        EnableItemButton.colors = colors;
    }
    #endregion
}
