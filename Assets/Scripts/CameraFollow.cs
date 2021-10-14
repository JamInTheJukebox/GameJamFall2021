using UnityEngine;
using System.Collections;

public class Camera_follow : MonoBehaviour
{
    public CharacterMovement target;
    public bool isFollowing = true;
    private float zPosition;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;
    public float maxSpeed = 40;

    void Awake()
    {
        zPosition = transform.position.z;
        target = FindObjectOfType<CharacterMovement>();

    }

    void LateUpdate()
    {
        var temp = target.transform.position;
        if (isFollowing)
        {
            temp = new Vector3(temp.x, temp.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothTime, maxSpeed);
        }

    }
}
