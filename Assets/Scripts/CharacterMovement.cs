using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    bool canMovePlayer = true;   
    public float horizontalSpeed = 8;
    Rigidbody2D rb;
    // Awake is called as soon as player presses play
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // setup delegate
        MasterUserInterface.instance.TimelineController.TimelineChangeListener += ToggleMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMovePlayer)
            MovePlayer();
    }

    private void MovePlayer()
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

    public void ToggleMovement(bool newState)
    {
        canMovePlayer = newState;
        if (!canMovePlayer)
            rb.velocity = new Vector2(0,rb.velocity.y);
    }
}
