using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacePlayer : MonoBehaviour
{
    [SerializeField] Transform movePoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = movePoint.position;
    }
}
