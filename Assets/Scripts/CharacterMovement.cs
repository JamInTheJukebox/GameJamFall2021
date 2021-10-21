using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    public float horizontalSpeed = 8;
    Rigidbody2D rb;
    // Awake is called as soon as player presses play
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var vel = Input.GetAxis("Horizontal") * horizontalSpeed;
        rb.velocity = new Vector2(vel, rb.velocity.y);
        // if the A key was pressed this frame
        if (vel < 0)
        {
            if (mySpriteRenderer != null)
            {
                mySpriteRenderer.flipX = true;
            }
        }
        if (vel > 0)
        {
            if (mySpriteRenderer != null)
            {
                mySpriteRenderer.flipX = false;
            }
        }
    }
}
