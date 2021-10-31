using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] float MinimumSquareDistance = 400;
    //protected UnityAction<ItemScriptable> interactAction;           // specify what kind of information we are passing in with this action
    [SerializeField] protected bool TouchToInteract;
    public UnityEvent onInteract;

    public virtual bool Interact(Vector2 targetPos)
    {
        Vector2 distVector = targetPos - (Vector2)transform.position;
        return Vector2.SqrMagnitude(distVector) < MinimumSquareDistance;
    }

    public virtual bool Interact(Vector2 targetPos, Items selectedItem)     // making items interact with stuff
    {
        return Interact(targetPos);
    }
    public abstract void executeInteractable();
}
