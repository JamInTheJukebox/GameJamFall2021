using UnityEngine;

[CreateAssetMenu(fileName = "ItemInformation", menuName = "ScriptableObjects/ItemScriptable", order = 1)]
public class ItemScriptable : ScriptableObject
{
    public Items itemType;

    public bool itemAvalible = true;

    public Sprite itemSprite;

    public Texture2D cursorImage;

    public int itemUses = 0;

}