public class Camera_follow : MonoBehaviour
{
    public Character_Controller target;
    public bool isFollowing = true;
    private float zPosition;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;
    public float maxSpeed = 40;

    void Awake()
    {
        zPosition = transform.position.z;
        target = FindObjectOfType<Character_Controller>();

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
