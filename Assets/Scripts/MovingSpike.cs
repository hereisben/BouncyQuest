using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    public float moveSpeed = 1.5f; // Speed of movement
    public float moveDistance = 2.5f; // How far it moves left & right
    public bool movingRight = true; // Direction flag
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Store original position
        SnapToGround(); // Auto-position on ground
    }

    void FixedUpdate()
    {
        MoveSpike();
    }

    void MoveSpike()
    {
        // Move left or right based on direction
        float movement = movingRight ? moveSpeed * Time.deltaTime : -moveSpeed * Time.deltaTime;
        transform.position += new Vector3(movement, 0, 0);

        // Flip the sprite when changing direction
        if (movingRight)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1); // Normal direction
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1); // Flipped when moving left
        }

        // Check if spike has moved too far from its start position
        if (Vector3.Distance(transform.position, startPosition) >= moveDistance)
        {
            movingRight = !movingRight; // Flip direction
        }
    }

    void SnapToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            float spikeHeight = GetComponent<SpriteRenderer>().bounds.size.y; // Get sprite height
            transform.position = new Vector3(transform.position.x, hit.point.y + (spikeHeight / 2) - 0.1f, transform.position.z);
        }
        else
        {
            Debug.LogWarning("Spike could not find the ground!");
        }
    }
}
