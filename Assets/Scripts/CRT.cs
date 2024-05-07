using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed = 5.0f;
    public float stopDistance = 1.0f;
    public float contactTimeToTrigger = 3.0f; 

    private Transform playerTransform;
    private float contactTimer = 0.0f; 
    private bool isInContact = false; 

    void Start()
    {
       
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure there's an object tagged 'Player'.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            if (distance > stopDistance)
            {
                
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }

            if (isInContact)
            {
                
                contactTimer += Time.deltaTime;

               
                if (contactTimer >= contactTimeToTrigger)
                {
                    OnPlayerKill();
                }
            }
            else
            {
               
                contactTimer = 0.0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = false; 
        }
    }

    private void OnPlayerKill()
    {
        Debug.Log("Player has been in contact for too long! Action triggered.");
        if (playerTransform != null)
        {
            
            Destroy(playerTransform.gameObject);

            
            Debug.Log("Player has been destroyed.");
        }
    }
}
