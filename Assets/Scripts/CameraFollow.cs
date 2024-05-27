using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform
    public float smoothSpeed = 0.125f; // How smoothly the camera follows the player
    public Vector3 offset; // Offset of the camera from the player

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired X position of the camera
            float desiredX = target.position.x + offset.x;

            // Calculate the current Y position of the camera
            float currentY = transform.position.y;

            // Smoothly move the camera horizontally
            float smoothedX = Mathf.Lerp(transform.position.x, desiredX, smoothSpeed * Time.deltaTime);

            // Set the camera position
            transform.position = new Vector3(smoothedX, currentY, transform.position.z);
        }
    }
}
