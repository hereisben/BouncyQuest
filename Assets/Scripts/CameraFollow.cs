using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform slime; // Assign Slime GameObject here
    public float smoothSpeed = 0.125f; // Speed of camera movement
    public Vector3 offset; // Offset from the slime's position

    public float minX = -9f; // Minimum X position for camera bounds
    public float maxX = 11.5f; // Maximum X position for camera bounds
    public float minY = -5.2f; // Minimum Y position for camera bounds
    public float maxY = 5.6f; // Maximum Y position for camera bounds

    private float camHalfWidth, camHalfHeight; // Half-width and half-height of the camera

    void Start()
    {
        // Get the camera's half-width and half-height based on its size
        Camera cam = GetComponent<Camera>();
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect; // Aspect ratio ensures correct width
    }

    void LateUpdate()
    {
        if (slime != null)
        {
            Vector3 desiredPosition = slime.position + offset;

            // Clamp position considering camera size
            float clampedX = Mathf.Clamp(desiredPosition.x, minX + camHalfWidth, maxX - camHalfWidth);
            float clampedY = Mathf.Clamp(desiredPosition.y, minY + camHalfHeight, maxY - camHalfHeight);

            Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
        }
    }
}
