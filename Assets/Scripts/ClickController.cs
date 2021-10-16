using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    // Update is called once per frame
    void Update() { 

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                var obj = hit.collider.GetComponent<Interactable>();
                if (!obj) { return; }
                if ((bool)obj?.Interact(mousePos2D))
                {
                    obj.executeInteractable();
                    Debug.Log("successful interaction!");
                }
            }
        }
    }
    
}

    // RaycastHit2D
    //Physics2D