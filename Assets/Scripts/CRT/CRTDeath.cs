using UnityEngine;

public class DestroyOnParryZoneContact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered with: {other.name}"); // Log which object is triggering the event

        // If the object collides with something tagged "ParryZone," destroy this GameObject
        if (other.CompareTag("ParryZone"))
        {
            Debug.Log("Contact with ParryZone detected.");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No ParryZone tag detected.");
        }
    }
}
