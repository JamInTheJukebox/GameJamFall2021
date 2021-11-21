using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : MonoBehaviour
{
    // Update is called once per frame
    public LayerMask InteractableLayer;

    void Update() {

        if (PreventClicking()) { return; }
        if (Input.GetMouseButton(0) && MasterUserInterface.instance.StoneController.isUsingStone)
        {
            MasterUserInterface.instance.StoneController.useStone();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            InteractWithObject(); 
        }
    }

    bool PreventClicking()
    {
        // do not do anything with interactables if you are over UI.
        try
        {
            return EventSystem.current.IsPointerOverGameObject() || MasterUserInterface.instance.PauseMenu.isPaused() || MasterUserInterface.instance.DialogueTyper.isTyping();
        }
        catch
        {
            Debug.Log("Click Controller.cs: A component to check for clicking is missing!");
        }
        return false;
    }

    void InteractWithObject()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, InteractableLayer);

        var itemController = MasterUserInterface.instance.ItemUIController;
        if (itemController.IsUsingItem())
        {
            if(!itemController.equippedItem.requiresInteractable)
            {
                print("using item");
                itemController.UseItem();
                return;
            }
        }
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            var obj = hit.collider.GetComponent<Interactable>();
            if (!obj) { return; }
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
                if ((bool)obj?.Interact(mousePos2D, itemController.GetItemTypeInUse()))
                {
                    itemController.UseItem(obj);
                    Debug.Log("successful item interaction!");
                }
            }
        }
    }
    
}

    // RaycastHit2D
    //Physics2D