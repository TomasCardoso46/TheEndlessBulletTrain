using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab; // Prefab for the object to grab
    [SerializeField] private float throwForce = 10f; // Force to apply when throwing the object

    public bool HasObject { get; private set; } = false; // Property to check if object is currently grabbed

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            HasObject = true; // Set HasObject to true when an object is in the trigger zone
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            HasObject = false; // Set HasObject to false when an object exits the trigger zone
        }
    }

    public void PlaceObject(Vector3 position, Quaternion rotation)
    {
        if (objectPrefab != null)
        {
            Instantiate(objectPrefab, position, rotation); // Instantiate object at given position
            HasObject = false; // Reset HasObject to false after placing the object
        }
    }

    public void ThrowObject(Vector3 position, Quaternion rotation)
    {
        if (objectPrefab != null)
        {
            GameObject thrownObject = Instantiate(objectPrefab, position, rotation); // Instantiate object at given position
            Rigidbody2D rb = thrownObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 throwDirection = new Vector2(transform.localScale.x, 0); // Assuming x scale < 0 when facing left
                rb.AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse); // Apply throwing force
                HasObject = false; // Reset HasObject to false after throwing the object
            }
        }
    }
}
