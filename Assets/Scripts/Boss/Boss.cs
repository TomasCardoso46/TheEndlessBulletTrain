using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public Animator animator;
    public float speed = 3.0f;
    public float followDistance = 0.5f;
    public float chargeDistance = 3f; // Distance at which the boss initiates a charged attack
    public float knockbackForce;
    public bool isBeingKnockedBack = false;
    public Rigidbody2D enemyRb;
    private Transform playerTransform;
    public PlayerMovement playerMovement;
    private PlayerBody PlayerBodyScript = null;
    public GameManager gameManagerScript;
    public float contactTimeThreshold = 3.0f;
    [SerializeField]
    public float contactTimer = 0.0f;
    
    public bool isInContact = false;
    

    private BossMoveSet bossMoveSet;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        

        bossMoveSet = GetComponent<BossMoveSet>();
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

                if (contactTimer >= contactTimeThreshold)
                {
                    resetTimer();
                    ApplyKnockbackToPlayer();
                    gameManagerScript.LoseHealth();
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
            if (direction.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    
                }
                else
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    
                }

            if (distance > followDistance)
            {
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;

                if (animator != null)
                {
                    animator.SetBool("IsMoving", true);
                }

                if (distance > chargeDistance) // Check if the distance is greater than chargeDistance
                {
                    bossMoveSet.TryInitiateChargedAttack(chargeDistance);
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
        notInContact();
        resetTimer();
        return;
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
    private IEnumerator Knockback(Vector2 direction)
    {
        isBeingKnockedBack = true;
        enemyRb.velocity = direction * knockbackForce;

        yield return new WaitForSeconds(0.5f); // Adjust this value as needed

        isBeingKnockedBack = false;
    }

    public void ApplyKnockbackToBoss(Vector2 direction)
    {
        if (!isBeingKnockedBack)
        {
            StartCoroutine(Knockback(direction));
        }
    }

    public void ApplyKnockbackToPlayer()
    {
        if (playerTransform != null && playerMovement != null)
        {
            Vector2 knockbackDirection = (playerTransform.position - transform.position).normalized;
            Vector2 knockbackForceVector = knockbackDirection * knockbackForce;
            playerMovement.ApplyKnockback(knockbackForceVector);
        }
    }
    
}
