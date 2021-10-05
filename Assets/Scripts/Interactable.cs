using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float MinimumSquareDistance = 400;
    public virtual bool Interact(Vector2 targetPos)
    {
        Vector2 distVector = targetPos - (Vector2)transform.position;
        return Vector2.SqrMagnitude(distVector) < MinimumSquareDistance;
    }

}
