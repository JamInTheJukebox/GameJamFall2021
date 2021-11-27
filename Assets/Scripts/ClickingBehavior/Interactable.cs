using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] float MinimumDistance = 30;
    //protected UnityAction<ItemScriptable> interactAction;           // specify what kind of information we are passing in with this action
    [SerializeField] protected bool TouchToInteract;
    public UnityEvent onInteract;

    public virtual bool Interact(Vector2 targetPos)
    {
        if (TouchToInteract) { return false; }
        float distance = Vector2.Distance((Vector2)targetPos, (Vector2)transform.position);
        return distance < MinimumDistance;
    }

    public virtual bool Interact(Vector2 targetPos, Items selectedItem)     // making items interact with stuff
    {
        return Interact(targetPos);
    }
    public abstract void executeInteractable();
}
