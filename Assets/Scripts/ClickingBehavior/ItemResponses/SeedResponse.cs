using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedResponse : GenericEvent
{
    public GameObject Plant;
    public Transform PlantParent;

    bool planted;
    [SerializeField] bool needsWater = true;
    bool watered;
    private void Awake()
    {
        watered = !needsWater;
    }

    public override bool Interact(Vector2 targetPos, Items selectedItem)        // did we successfully interact with the item?
    {
        if (base.Interact(targetPos) && (RequiredItemToInteract == selectedItem || selectedItem == Items.waterCan))
        {
            return true; // failed to get the item
        }

        // failed to get the item   
        return false;
    }
    public override bool Interact(Vector2 targetPos)
    {
        return base.Interact(targetPos);
    }
    public override void executeInteractable()
    {

    }
    public override int executeInteractable(ItemScriptable item)
    {
        // we'll probably use this to update the status of this gameobject in all 3 timelines.
        if (item.itemType == Items.seed)
        {
            if (!planted)
            {
                planted = true;
                ChangeState();
                return 1;
            }
        }
        else if (item.itemType == Items.waterCan)
        {
            if (needsWater)
            {
                watered = true;
                ChangeState();
                return 1;
            }
        }

        return 0;
    }   

    public void ChangeState()       // make this timeline dependent
    {
        if(watered && planted)
        {
            base.executeInteractable();
            if(Plant != null && !Plant.activeSelf)
            {
                bool activatePlant = true;
                foreach(Transform plant in PlantParent)
                {
                    activatePlant = activatePlant && !plant.gameObject.activeSelf;
                }
                print(activatePlant);
                Plant.SetActive(activatePlant);
            }
        }
    }

    public void TurnAllOffExcept(GameObject root)
    {
        foreach(Transform plant_ in PlantParent)
        {
            GameObject plant = plant_.gameObject;
            if (plant != root)
                plant.SetActive(false);         // turn off all gameobjects except for the one we enter in this function        
            else
                plant.SetActive(true);
        }
    }
}