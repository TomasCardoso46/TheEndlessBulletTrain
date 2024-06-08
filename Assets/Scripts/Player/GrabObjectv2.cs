using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [SerializeField] private GameObject placeDownExtintor; // Prefab to spawn
    [SerializeField] private GameObject throwExtintor; // Prefab to throw
    [SerializeField] private GameObject placeDownMala; // Prefab to spawn
    [SerializeField] private GameObject throwMala; // Prefab to throw
    [SerializeField] private GameObject spawnPoint; // Primary spawn point for the new object
    [SerializeField] private GameObject alternateSpawnPoint; // Alternate spawn point for the new object
    [SerializeField] private float throwForce = 10f; // Force to apply when throwing the object

    [SerializeField] private bool canGrab = false;
    [SerializeField] public bool hasObject = false;
    private GameObject grabbedObject; // Reference to the grabbed object
    private string grabbedTag; // Tag of the grabbed object

    private void Start()
    {
        // Initial debug message
        Debug.Log("GrabObject script started.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Extintor") || other.CompareTag("Mala"))
        {
            canGrab = true;
            grabbedObject = other.gameObject;
            grabbedTag = other.tag;
            Debug.Log(other.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Extintor") || other.CompareTag("Mala"))
        {
            canGrab = false;
            grabbedObject = null;
            grabbedTag = null;
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
                if (grabbedTag == "Extintor")
                {
                    ThrowExtintor(alternateSpawnPoint.transform.position, alternateSpawnPoint.transform.rotation);
                }
                else if (grabbedTag == "Mala")
                {
                    ThrowMala(alternateSpawnPoint.transform.position, alternateSpawnPoint.transform.rotation);
                }
                hasObject = false;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && spawnPoint != null)
            {
                if (grabbedTag == "Extintor")
                {
                    PlaceDownExtintor(spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                else if (grabbedTag == "Mala")
                {
                    PlaceDownMala(spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                hasObject = false;
            }
        }
    }

    private void PlaceDownExtintor(Vector3 position, Quaternion rotation)
    {
        if (placeDownExtintor != null)
        {
            grabbedObject = Instantiate(placeDownExtintor, position, rotation);
            Debug.Log("Extintor placed down at primary spawn point.");
        }
        else
        {
            Debug.LogError("placeDownExtintor is not assigned.");
        }
    }

    private void ThrowExtintor(Vector3 position, Quaternion rotation)
    {
        if (throwExtintor != null)
        {
            grabbedObject = Instantiate(throwExtintor, position, rotation);
            Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Set gravity scale to 0 to ignore gravity
                rb.gravityScale = 0;

                // Calculate throw direction based on player's direction
                Vector2 throwDirection = new Vector2(transform.localScale.x, 0); // Assuming x scale < 0 when facing left

                // Apply force in the calculated direction
                rb.AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse);
                Debug.Log("Extintor spawned and thrown from alternate spawn point.");
            }
            else
            {
                Debug.LogError("Spawned object does not have a Rigidbody2D component.");
            }
        }
        else
        {
            Debug.LogError("throwExtintor is not assigned.");
        }
    }

    private void PlaceDownMala(Vector3 position, Quaternion rotation)
    {
        if (placeDownMala != null)
        {
            grabbedObject = Instantiate(placeDownMala, position, rotation);
            Debug.Log("Mala placed down at primary spawn point.");
        }
        else
        {
            Debug.LogError("placeDownMala is not assigned.");
        }
    }

    private void ThrowMala(Vector3 position, Quaternion rotation)
    {
        if (throwMala != null)
        {
            grabbedObject = Instantiate(throwMala, position, rotation);
            Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Set gravity scale to 0 to ignore gravity
                rb.gravityScale = 0;

                // Calculate throw direction based on player's direction
                Vector2 throwDirection = new Vector2(transform.localScale.x, 0); // Assuming x scale < 0 when facing left

                // Apply force in the calculated direction
                rb.AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse);
                Debug.Log("Mala spawned and thrown from alternate spawn point.");
            }
            else
            {
                Debug.LogError("Spawned object does not have a Rigidbody2D component.");
            }
        }
        else
        {
            Debug.LogError("throwMala is not assigned.");
        }
    }

    public void grabIsFalse()
    {
        hasObject = false;
    }
}
