using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float horizontalSpeed = 8;
    Rigidbody2D rb;

    // Awake is called as soon as player presses play
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var vel = Input.GetAxis("Horizontal") * horizontalSpeed;
        rb.velocity = new Vector2(vel, rb.velocity.y);
    }
}
