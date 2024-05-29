using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    // Reference to the position where the object will be held
    public Transform grabbingPosition;

    // Reference to the player's feet position
    public Transform playerFeet;
    private bool objectInHand = false;
    private bool canGrab = false;
    [SerializeField]
    private int throwForce = 5;
    private GameObject objectToGrab = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger has the "Grab" tag
        if (other.CompareTag("Grab"))
        {
            canGrab = true;
            Debug.Log("Object entered grabbing area.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting the trigger has the "Grab" tag
        if (other.CompareTag("Grab"))
        {
            canGrab = false;
            Debug.Log("Object left grabbing area.");
        }
    }

    private void Update()
    {
        if (canGrab && Input.GetKeyDown(KeyCode.Q) && objectToGrab == null)
        {
            GrabObject();
        }
        
        else if (canGrab && Input.GetKeyDown(KeyCode.Q) && objectToGrab != null)
        {
            ReleaseObject();
        }

        if (objectInHand && Input.GetKeyDown(KeyCode.T) && objectToGrab == null)
        {
            ThrowObject();
        }
    }

    private void GrabObject()
    {
        if (objectToGrab == null)
        {
            objectToGrab = GetClosestObjectWithTag("Grab");
            if (objectToGrab != null)
            {
                objectToGrab.transform.position = grabbingPosition.position;
                objectToGrab.transform.SetParent(grabbingPosition);
                objectInHand = true;
                Debug.Log("Object grabbed.");
            }
        }
    }
    private void ThrowObject()
    {
        
        if (objectInHand && objectToGrab != null)
        {
            // Detach the object from the player
            objectToGrab.transform.SetParent(null);

            // Enable the Rigidbody2D component to allow physics interactions
            Rigidbody2D rb = objectToGrab.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = objectToGrab.AddComponent<Rigidbody2D>(); // Add Rigidbody2D if it doesn't exist
            }
            rb.isKinematic = false; // Ensure the object is affected by physics
            rb.velocity = Vector2.zero; // Reset velocity before applying force

            // Apply force to throw the object
            Vector2 throwDirection = new Vector2(transform.localScale.x, 0); // Adjust the direction based on the player's facing direction
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            // Clear the held object reference and update the boolean
            
            objectInHand = false;
        }
       
    }

    private void ReleaseObject()
    {
        if (objectToGrab != null)
        {
            objectToGrab.transform.SetParent(null);
            Vector3 releasePosition = objectToGrab.transform.position;
            releasePosition.y = playerFeet.position.y;
            objectToGrab.transform.position = releasePosition;
            objectInHand = false;
            Debug.Log("Object released and Y position set to player's feet.");
            objectToGrab = null;
        }
    }

    private GameObject GetClosestObjectWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = grabbingPosition.position;

        foreach (GameObject obj in objectsWithTag)
        {
            float distance = Vector3.Distance(obj.transform.position, currentPosition);
            if (distance < closestDistance)
            {
                closestObject = obj;
                closestDistance = distance;
            }
        }

        return closestObject;
    }
}
