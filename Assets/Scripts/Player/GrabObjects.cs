using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    // Reference to the position where the object will be held
    public Transform grabbingPosition;

    // Reference to the player's feet position
    public Transform playerFeet;

    private bool canGrab = false;
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
                Debug.Log("Object grabbed.");
            }
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
