using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    // Reference to the ToggleVisibility script attached to the object you want to toggle visibility for
    public ToggleVisibility toggleVisibilityScript;
    private bool canGrab = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Grab"))
        {
            canGrab = true;
            Debug.Log("Player entered grabbing area.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Grab"))
        {
            canGrab = true;
            Debug.Log("Player left grabbing area.");
        }
    }
    private void Update()
    {
        if (canGrab && Input.GetKeyDown(KeyCode.Q)) 
        { 

        }
    }
}
