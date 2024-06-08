using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [SerializeField] private GameObject extintorPrefab; // Prefab for the extintor
    [SerializeField] private GameObject malaPrefab; // Prefab for the mala
    [SerializeField] private GameObject spawnPoint; // Spawn point for placing down objects
    [SerializeField] private GameObject extintorThrowable; // Prefab for throwing the extintor
    [SerializeField] private GameObject malaThrowable; // Prefab for throwing the mala
    [SerializeField] private float throwForce = 10f; // Force to apply when throwing the object
    [SerializeField] private bool canGrabExtintor = false;
    [SerializeField] private bool canGrabMala = false;
    [SerializeField] public bool hasExtintor = false; // Flag to indicate if player has the extintor
    [SerializeField] public bool hasMala = false; // Flag to indicate if player has the mala

    private GameObject currentObject; // The object currently in contact with the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Extintor"))
        {
            canGrabExtintor = true;
            currentObject = other.gameObject; // Set the current object to the one in contact
        }
        else if (other.CompareTag("Mala"))
        {
            canGrabMala = true;
            currentObject = other.gameObject; // Set the current object to the one in contact
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Extintor"))
        {
            canGrabExtintor = false;
        }
        else if (other.CompareTag("Mala"))
        {
            canGrabMala = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (canGrabExtintor && !hasExtintor) // Check if can grab and doesn't already have extintor
            {
                DestroyObject(currentObject); // Destroy the current object
                hasExtintor = true; // Set flag to true
            }
            else if (hasExtintor)
            {
                RespawnObject(extintorPrefab); // Respawn the extinguisher if already has it
                DoesntHaveExtintor();
            }

            if (canGrabMala && !hasMala) // Check if can grab and doesn't already have mala
            {
                DestroyObject(currentObject); // Destroy the current object
                hasMala = true; // Set flag to true
            }
            else if (hasMala)
            {
                RespawnObject(malaPrefab); // Respawn the mala if already has it
                DoesntHaveMala();
            }
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            if (hasExtintor)
            {
                ThrowObject(extintorThrowable);
                DoesntHaveExtintor();
            }
            else if (hasMala)
            {
                ThrowObject(malaThrowable);
                DoesntHaveMala();
            }
        }
    }


    private void DestroyObject(GameObject obj)
    {
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    private void ThrowObject(GameObject throwablePrefab)
    {
        if (throwablePrefab != null)
        {
            GameObject thrownObject = Instantiate(throwablePrefab, transform.position, transform.rotation);
            Rigidbody2D rb = thrownObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 throwDirection = new Vector2(transform.localScale.x, 0);
                rb.AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse);
            }
        }
    }

    private void RespawnObject(GameObject prefab)
    {
        if (prefab != null && spawnPoint != null)
        {
            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
        }
    }

    public void DoesntHaveExtintor()
    {
        hasExtintor = false;
    }
    public void DoesntHaveMala()
    {
        hasMala = false;
    }
}
