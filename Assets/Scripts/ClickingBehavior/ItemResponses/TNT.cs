using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    private bool OnTrackToBlowUp;
    private Animator tntAnimator;
    [SerializeField] float ExplosionTimer = 7;
    [SerializeField] CircleCollider2D BlastRange;

    // instance of this script for enemies and bullets to call.

    private Vector3 TargetPosition;             // the position the camera should always be 'focused' in. The Screenshake works by 
                                                // generating noise for the camera to move around this position.
    [Header("TNT Shake Effect")]
    [SerializeField] float ShakePower;
    [SerializeField] bool VibrateOnLit;

    private void Awake()
    {
        tntAnimator = GetComponent<Animator>();
        tntAnimator.enabled = false;    
        TargetPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartBombTimer();
        }
        transform.position = TargetPosition;
    }

    private void LateUpdate()                   // offset the position of the camera if the camera is shaking.
    {
        if (VibrateOnLit && OnTrackToBlowUp)
        {
            float x_Offset = GetRandomOffset();
            float y_Offset = GetRandomOffset();

            transform.position += new Vector3(x_Offset, y_Offset, 0);
        }
    }

    public void StartBombTimer()
    {
        if (OnTrackToBlowUp) { return; }
        OnTrackToBlowUp = true;
        Invoke("Explode", ExplosionTimer);
        tntAnimator.enabled = true;
    }

    private float GetRandomOffset()
    {
        return Random.Range(-1f, 1f) * ShakePower;
    }
    
    private void Explode()
    {
        // spawn vfx here
        BlastRange.enabled = true;
        Destroy(transform.parent.gameObject,0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)     // explode nearby TNTs
    {
        if (BlastRange.enabled) { return; } // cannot detonate self
        if (collision.tag == Tags.TNT && gameObject != collision.transform.parent.gameObject)      
        {
            print(collision.transform.parent.gameObject);
            print(gameObject);
            StartBombTimer();
        }
    }
}

