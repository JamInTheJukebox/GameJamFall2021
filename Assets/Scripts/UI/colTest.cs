using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Test1>())
        {
            collision.gameObject.GetComponent<Test1>().fun();
        }
    }
}
