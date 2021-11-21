using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform_People : MonoBehaviour
{
    public Transform finalPosition;
    public Transform finalSize;
    public void move()
    {
        transform.position = finalPosition.position;
    }

}
//lossy for parent, local for child