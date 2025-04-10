using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f; // Speed of platform movement
    public float moveDistance = 3f; // How far the platform moves

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Store the starting position
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, moveDistance * 2) - moveDistance;
        transform.position = new Vector3(startPos.x + movement, transform.position.y, transform.position.z);
    }
}
