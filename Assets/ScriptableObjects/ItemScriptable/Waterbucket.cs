using UnityEngine;
[CreateAssetMenu(fileName = "ItemInformation", menuName = "ScriptableObjects/ItemScriptable/Waterbucket", order = 2)]
public class Waterbucket : ItemScriptable
{
    [SerializeField] Texture2D FullWaterBucketCursor;
    [SerializeField] Sprite FullWaterBucketSprite;
    [SerializeField] Texture2D EmptyWaterBucketCursor;
    [SerializeField] Sprite EmptyWaterBucketSprite;
    [SerializeField] bool m_Filled;
    bool Filled
    {
        get { return m_Filled; }
        set
        {
            if(value != m_Filled)
            {
                m_Filled = value;
                SetCursorImage();
                SetItemImage();
            }
        }
    }

    public override void SetCursorImage()
    {
        cursorImage = (Filled) ? FullWaterBucketCursor : EmptyWaterBucketCursor;
    }

    public override void SetItemImage()
    {
        itemSprite = (Filled) ? FullWaterBucketSprite : EmptyWaterBucketSprite;
    }

    public override void UseItem(Interactable well_Interactable)
    {
        try
        {
            Well well = (Well)well_Interactable;
            var newState = well.ChangeFilledState(Filled);
            if (newState)
                Filled = !Filled;
        }
        catch
        {
            Debug.LogWarning("WaterBucket.cs error: CAST UNSUCCESSFUL");
        }
    }

    public void init(Waterbucket SO)
    {
        base.init(SO);
        FullWaterBucketCursor = SO.FullWaterBucketCursor;
        FullWaterBucketSprite = SO.FullWaterBucketSprite;
        EmptyWaterBucketCursor = SO.EmptyWaterBucketCursor;
        EmptyWaterBucketSprite = SO.EmptyWaterBucketSprite;
        Filled = SO.Filled;
    }
}
