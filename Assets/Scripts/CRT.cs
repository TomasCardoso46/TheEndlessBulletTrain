using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 5.0f; 
    public float followDistance = 0.5f; 
    public float contactTimeThreshold = 3.0f; 
    private float contactTimer = 0.0f;
    private bool isInContact = false;


    private Transform playerTransform; 

    void Start()
    {
        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
           
            Vector3 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude;

            
            if (distance > followDistance)
            {
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;
            }
        }

        if (isInContact)
        {
            
            contactTimer += Time.deltaTime;

            
            if (contactTimer >= contactTimeThreshold)
            {
                DestroyPlayer();
            }
        }
        else
        {

            contactTimer = 0.0f;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = true; 
            Debug.Log("Está em contacto");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInContact = false;
            Debug.Log("Não está em contacto");
        }
    }
    void DestroyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Destroy(player);
        }
    }
}

    

