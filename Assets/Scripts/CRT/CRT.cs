using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;
    public float followDistance = 0.5f;
    public Rigidbody2D enemyRb;
    private Transform playerTransform;
    public PlayerMovement playerMovement;
    public PlayerBody PlayerBodyScript;
    public float contactTimeThreshold = 3.0f;
    [SerializeField]
    public float contactTimer = 0.0f;
    public float EKBForce;
    public float EKBCounter;
    public float EKBTotalTime;
    public bool EKnockFromRight;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        
        if (playerObject != null && EKBCounter <= 0)
            {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player GameObject has the 'Player' tag.");
        }

        if (animator == null)
        {
            Debug.LogError("Animator not assigned. Please assign an Animator component.");
        }

        if (PlayerBodyScript == null)
        {
            Debug.LogError("PlayerBodyScript not assigned. Please assign a PlayerBody script in the Inspector.");
        }
    }

    void Update()
    {
        if (PlayerBodyScript != null)
        {
            if (PlayerBodyScript.isInContact)
            {
                contactTimer += Time.deltaTime;

                if (contactTimer >= contactTimeThreshold - 3f)
                {
                    animator.SetBool("IsAttacking", true);
                }

                if (contactTimer >= contactTimeThreshold)
                {
                    playerMovement.KBCounter = playerMovement.KBTotalTime;
                    PlayerBodyScript.health -= 1;
                    contactTimer = 0.0f;
                    animator.SetBool("IsAttacking", false);
                }
            }
            else
            {
                contactTimer = 0.0f;
            }
        }

        if (playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude;

            if (distance > followDistance)
            {
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;

                if (animator != null)
                {
                    animator.SetBool("IsMoving", true);
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("IsMoving", false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerBodyScript != null)
            {
                PlayerBodyScript.isInContact = true;
                Debug.Log("Contact with player started.");
            }
            else
            {
                Debug.LogError("PlayerBodyScript is null in OnTriggerEnter2D.");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        PlayerBodyScript.isInContact = false;
        contactTimer = 0.0f;
        return;
    }
}
