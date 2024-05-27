using UnityEngine;

public class DestroyOnParryZoneContact : MonoBehaviour
{
    [SerializeField]
    private float misfortune;
    public PlayerBody PlayerBodyScript;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered with: {other.name}"); // Log which object is triggering the event

        // Log the tag of the other object
        Debug.Log($"Other object's tag: {other.tag}");

        // Check if PlayerBodyScript is assigned
        if (PlayerBodyScript == null)
        {
            Debug.LogError("PlayerBodyScript is not assigned!");
            return;
        }

        // Log the contactTimer value
        Debug.Log($"contactTimer: {PlayerBodyScript.contactTimer}");

        if (other.CompareTag("ParryZone"))
        {
            Debug.Log("1");


        }
        if (PlayerBodyScript.contactTimer >= 1.5f)
        {
            Debug.Log("2");
        }

        // If the object collides with something tagged "ParryZone," destroy this GameObject
        if (other.CompareTag("ParryZone") && PlayerBodyScript.contactTimer >= 1.5f)
        {
            Debug.Log("Contact with ParryZone detected.");
            PlayerBodyScript.contactTimer = 0;
            misfortune += 1;
            
        }
        else
        {
            Debug.Log("No ParryZone tag detected or contactTimer is too low.");
        }
        if (misfortune >= 3)
        {
            Destroy(gameObject);
        }
    }
}
