using UnityEngine;

public class HidingPlaceDetector : MonoBehaviour
{
    // Reference to the ToggleVisibility script attached to the object you want to toggle visibility for
    public ToggleVisibility toggleVisibilityScript;
    public Animator animator;
    private void Start()
    {
        if (toggleVisibilityScript == null)
        {
            Debug.LogError("ToggleVisibility script reference is not set!");
        }
    }

    public void hiddenAnimation()
    {
        animator.SetBool("isHiden", true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Set isInHidingPlace to true when the player enters the trigger
            toggleVisibilityScript.isInHidingPlace = true;
            Debug.Log("Player entered hiding place.");
        }
        else
        {
            Debug.Log("Non-player entered hiding place: " + other.name);
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
        else
        {
            Debug.Log("Non-player left hiding place: " + other.name);
        }
    }
}
