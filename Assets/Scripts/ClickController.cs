using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    // Update is called once per frame
    public LayerMask InteractableLayer;

    void Update() { 

        if (Input.GetMouseButtonDown(0))
        {
            InteractWithObject(); 
        }
    }

    void InteractWithObject()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, InteractableLayer);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            var obj = hit.collider.GetComponent<Interactable>();
            if (!obj) { return; }
            var itemController = MasterUserInterface.instance.ItemUIController;
            if (!itemController.IsUsingItem())                       // pick up objects, interact with dialogue prompts.
            {
                if ((bool)obj?.Interact(mousePos2D))
                {
                    obj.executeInteractable();
                    Debug.Log("successful interaction!");
                }
            }
            else
            {
                // if we have an item, lend control of what happens to the ItemUIController.
                if ((bool)obj?.Interact(mousePos2D, itemController.GetItemInUse()))
                {
                    itemController.UseItem(obj);
                    Debug.Log("successful item interaction!");
                }
            }
        }
        else if(MasterUserInterface.instance.ItemUIController.GetItemInUse() == Items.plasmaBall)
        {
            MasterUserInterface.instance.ItemUIController.UseItem();
        }
    }
    
}

    // RaycastHit2D
    //Physics2D