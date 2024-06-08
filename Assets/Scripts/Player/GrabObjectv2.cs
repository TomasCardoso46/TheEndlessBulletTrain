using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [SerializeField] private GameObject PlaceDownPrefab; // Prefab to spawn
    [SerializeField] private GameObject ThrowPrefab; // Prefab to throw
    [SerializeField] private GameObject spawnPoint; // Primary spawn point for the new object
    [SerializeField] private GameObject alternateSpawnPoint; // Alternate spawn point for the new object
    [SerializeField] private float throwForce = 10f; // Force to apply when throwing the object

    [SerializeField] private bool canGrab = false;
    [SerializeField] public bool hasObject = false;
    private GameObject grabbedObject; // Reference to the grabbed object

    private void Start()
    {
        // Initial debug message
        Debug.Log("GrabObject script started.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Extintor"))
        {
            canGrab = true;
            grabbedObject = other.gameObject;
            Debug.Log("Extintor");
        }

        if (other.CompareTag("Mala"))
        {
            canGrab = true;
            grabbedObject = other.gameObject;
            Debug.Log("Mala");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Extintor"))
        {
            canGrab = false;
            grabbedObject = null;
            Debug.Log("Object left grabbing area.");
        }

        if (other.CompareTag("Mala"))
        {
            canGrab = false;
            grabbedObject = null;
            Debug.Log("Object left grabbing area.");
        }
    }

    private void Update()
    {
        if (canGrab && Input.GetKeyDown(KeyCode.Q) && grabbedObject != null && !hasObject)
        {
            Destroy(grabbedObject);
            hasObject = true;
            Debug.Log("Object destroyed.");
        }
        else if (hasObject)
        {
            if (Input.GetKeyDown(KeyCode.T) && alternateSpawnPoint != null)
            {
                ThrowObject(alternateSpawnPoint.transform.position, alternateSpawnPoint.transform.rotation);
                hasObject = false;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && spawnPoint != null)
            {
                PlaceDownObject(spawnPoint.transform.position, spawnPoint.transform.rotation);
                hasObject = false;
            }
        }
    }

    private void PlaceDownObject(Vector3 position, Quaternion rotation)
    {
        if (PlaceDownPrefab != null)
        {
            grabbedObject = Instantiate(PlaceDownPrefab, position, rotation);
            Debug.Log("Object placed down at primary spawn point.");
        }
        else
        {
            Debug.LogError("PlaceDownPrefab is not assigned.");
        }
    }

    private void ThrowObject(Vector3 position, Quaternion rotation)
    {
        if (ThrowPrefab != null)
        {
            grabbedObject = Instantiate(ThrowPrefab, position, rotation);
            Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Set gravity scale to 0 to ignore gravity
                rb.gravityScale = 0;

                // Calculate throw direction based on player's direction
                Vector2 throwDirection = new Vector2(transform.localScale.x, 0); // Assuming x scale < 0 when facing left

                // Apply force in the calculated direction
                rb.AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse);
                Debug.Log("Object spawned and thrown from alternate spawn point.");
            }
            else
            {
                Debug.LogError("Spawned object does not have a Rigidbody2D component.");
            }
        }
        else
        {
            Debug.LogError("ThrowPrefab is not assigned.");
        }
    }

    public void grabIsFalse ()
    {
        hasObject = false;
    }
}