using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    private Renderer rend;
    private bool isVisible = true;
    public bool isInHidingPlace = false;
    public string tag1 = "Player";
    public string tag2 = "PlayerHidden";

    // Reference to the player's movement script (assuming it's called PlayerMovement)
    public PlayerMovement playerMovementScript;

    void Start()
    {
        // Get the renderer component of the GameObject
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Check for input to toggle visibility only if isInHidingPlace is true
        if (isInHidingPlace && Input.GetKeyDown(KeyCode.E))
        {
            Toggle();
            ToggleTag();
        }

        // If the player is invisible, disable movement controls
        if (!isVisible)
        {
            playerMovementScript.enabled = false;
        }
        else
        {
            playerMovementScript.enabled = true;
        }
    }

    void Toggle()
    {
        // Toggle the visibility state
        isVisible = !isVisible;

        // Set the renderer's enabled state based on the visibility state
        rend.enabled = isVisible;

        // If the object is now visible, print a message
        if (isVisible)
        {
            Debug.Log("Object is now visible.");
        }
        // If the object is now invisible, print a message
        else
        {
            Debug.Log("Object is now invisible.");
        }
    }
    void ToggleTag()
    {
        // Check the current tag and toggle it
        if (gameObject.tag == tag1)
        {
            gameObject.tag = tag2;
        }
        else
        {
            gameObject.tag = tag1;
        }
    }
}
