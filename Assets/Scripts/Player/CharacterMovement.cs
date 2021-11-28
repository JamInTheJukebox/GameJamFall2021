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
    [Header("Short Hop above ledge")]
    public float TimeToRunIntoWall = 0.18f;
    private float CurrentTimeToRunIntoWall; 
    public Transform FeetColliderPos; Vector2 FeetPos;
    public Transform KneeColliderPos; Vector2 KneePos;  // do not hop above anything if the knee collider is colliding with a wall.
    bool HoppingAvailable;
    bool CanHopAgain = true;
    public LayerMask WallCollider;
    public float HopImpulse = 10;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        FeetPos = FeetColliderPos.localPosition;
        KneePos = KneeColliderPos.localPosition;
    }

    private void Start()
    {
        // setup delegate
        MasterUserInterface.instance.TimelineController.TimelineChangeListener += ToggleMovement;
    }

    public void Update()
    {
        if (CanHopAgain)
        {
            if (Input.GetAxis(Axis.HORIZONTAL) != 0 && !HoppingAvailable)
            {
                CheckHop();
            }
            if (HoppingAvailable)
            {
                Hop();
            }
            else
            {
                CurrentTimeToRunIntoWall = 0;
                HoppingAvailable = false;
            }

        }

        {
            Vector2 feetPos = new Vector2((mySpriteRenderer.flipX) ? FeetPos.x * -1 : FeetPos.x * 1, FeetPos.y);
            Vector2 kneePos = new Vector2((mySpriteRenderer.flipX) ? KneePos.x * -1 : KneePos.x * 1, KneePos.y);
            FeetColliderPos.localPosition = feetPos;
            KneeColliderPos.localPosition = kneePos;
        }
    }

    private void Hop()
    {
        if (HoppingAvailable && CurrentTimeToRunIntoWall > 0)
        {
            CurrentTimeToRunIntoWall -= Time.deltaTime;
        }
        else if (HoppingAvailable && CurrentTimeToRunIntoWall <= 0)
        {
            CurrentTimeToRunIntoWall = 0;
            HoppingAvailable = false;
            CanHopAgain = false;
            Invoke("ResetHop", 1f);
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            print("Hopping!@");
        }
    }

    private void CheckHop()
    {
        bool FeetAgainstWall = Physics2D.OverlapCircle(FeetColliderPos.position, 0.4f, WallCollider);
        bool KneeAgainstWall = Physics2D.OverlapCircle(KneeColliderPos.position, 0.4f, WallCollider);
        print(KneeAgainstWall + " h "+  FeetAgainstWall);

        if (FeetAgainstWall && !KneeAgainstWall)
        {
            CurrentTimeToRunIntoWall = TimeToRunIntoWall;
            HoppingAvailable = true;
        }
    }

    private void ResetHop()
    {
        CanHopAgain = true;
    }
    // Update is called once per frame
    void FixedUpdate()
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
        if (newState)
        {
            mySpriteRenderer.sortingLayerName = "Player";
        }
        canMovePlayer = newState;
        //mySpriteRenderer.sortingLayerName = "TimeLineTransition";
        if (!canMovePlayer)
            rb.velocity = new Vector2(0,rb.velocity.y);
    }
    private void OnDisable()
    {
        MasterUserInterface.instance.TimelineController.TimelineChangeListener -= ToggleMovement;
    }
}
