using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;
    public float followDistance = 0.5f;
    public Rigidbody2D enemyRb;
    private Transform playerTransform;
    public PlayerMovement playerMovement;
    private PlayerBody PlayerBodyScript = null;
    public float contactTimeThreshold = 3.0f;
    [SerializeField]
    public float contactTimer = 0.0f;
    public bool isInContact = false;

    public float knockbackForce = 5.0f; // Adjust this as needed

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            playerMovement = playerObject.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        if (PlayerBodyScript == null)
        {
            PlayerBodyScript = FindObjectOfType<PlayerBody>();
            if (PlayerBodyScript == null)
            {
                return;
            }
        }
        if (PlayerBodyScript != null)
        { 
            if (isInContact)
            {
                contactTimer += Time.deltaTime;

                if (contactTimer >= contactTimeThreshold - 3f)
                {
                    animator.SetBool("IsAttacking", true);
                }

                if (contactTimer >= contactTimeThreshold)
                {         
                    resetTimer();
                    
                    PlayerBodyScript.loseHealth();
                    ApplyKnockbackToPlayer();
                    animator.SetBool("IsAttacking", false);
                    return;   
                }
            }
            else
            {
                resetTimer();
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
                inContact();
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
        if (other.CompareTag("Player"))
        {
            notInContact();
            resetTimer();
        }
    }

    public void resetTimer()
    {
        contactTimer = 0.0f;
    }

    public void inContact()
    {
        isInContact = true;
    }

    public void notInContact()
    {
        isInContact = false;
    }

    private void ApplyKnockbackToPlayer()
    {
        if (playerTransform != null && playerMovement != null)
        {
            Vector2 knockbackDirection = (playerTransform.position - transform.position).normalized;
            Vector2 knockbackForceVector = knockbackDirection * knockbackForce;
            playerMovement.ApplyKnockback(knockbackForceVector);
        }
    }

    // Method to apply knockback to the enemy
    public void ApplyKnockback(Vector2 knockbackForce)
    {
        enemyRb.AddForce(knockbackForce, ForceMode2D.Impulse);
    }
}
