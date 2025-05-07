using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MovementType { Horizontal, Vertical }
    public MovementType movementType = MovementType.Horizontal;

    public float speed = 2f;
    public float moveDistance = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, moveDistance * 2) - moveDistance;

        if (movementType == MovementType.Horizontal)
        {
            transform.position = new Vector3(startPos.x + movement, startPos.y, startPos.z);
        }
        else if (movementType == MovementType.Vertical)
        {
            transform.position = new Vector3(startPos.x, startPos.y + movement, startPos.z);
        }
    }
}
