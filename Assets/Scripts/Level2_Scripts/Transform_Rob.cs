using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform_Rob : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
/*        if (mySpriteRenderer = ) 
            {
            // Location of robber
            UnityEditor.TransformWorldPlacementJSON:{ "position":{ "x":-6.247673511505127,"y":-4.737466812133789,"z":0.0},"rotation":{ "x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{ "x":0.40141361951828005,"y":2.030445098876953,"z":1.0} }

            // Location of pesant
            UnityEditor.TransformWorldPlacementJSON:{ "position":{ "x":4.582684516906738,"y":-1.1679596900939942,"z":0.0},"rotation":{ "x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{ "x":0.40141361951828005,"y":2.030445098876953,"z":1.0} }
            }*/
    }
}
