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

    void Awake()
    {
        zPosition = transform.position.z;
        target = FindObjectOfType<CharacterMovement>().transform;

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
            transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothTime, maxSpeed);
        }

    }
}
