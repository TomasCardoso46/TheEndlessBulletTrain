using UnityEngine;

public class HidingPlaceDetector : MonoBehaviour
{
    // Reference to the ToggleVisibility script attached to the object you want to toggle visibility for
    public ToggleVisibility toggleVisibilityScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Set isInHidingPlace to true when the player enters the trigger
            toggleVisibilityScript.isInHidingPlace = true;
            Debug.Log("Player entered hiding place.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Set isInHidingPlace to false when the player exits the trigger
            toggleVisibilityScript.isInHidingPlace = false;
            Debug.Log("Player left hiding place.");
        }
    }
}
