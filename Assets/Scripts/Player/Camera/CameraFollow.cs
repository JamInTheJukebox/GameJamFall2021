using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool isFollowing = true;
    private float zPosition;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;
    public float maxSpeed = 40;
    public bool ScreenBounded = false;      // prevents the screen from moving outside the background.
    public Vector2 BoundsX;

    void Awake()
    {
        zPosition = transform.position.z;
        target = FindObjectOfType<CharacterMovement>().transform;
      
        if (target)
            transform.position = new Vector3(target.position.x, target.position.y, zPosition);
    }

    void LateUpdate()
    {
        if(!target) {
            Debug.LogWarning("CameraFollow.cs: Unassigned target. Camera will not follow anything!");
            return;
        }
        var temp = target.transform.position;
        if (isFollowing)
        {
            temp = new Vector3(temp.x, temp.y, transform.position.z);
            if(ScreenBounded)
                temp.x = Mathf.Clamp(temp.x, BoundsX.x, BoundsX.y);
            //temp.y = Mathf.Clamp(temp.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
            transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothTime, maxSpeed);
        }
    }
}
