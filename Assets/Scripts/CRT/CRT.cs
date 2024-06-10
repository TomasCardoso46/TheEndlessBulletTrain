using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;
    public float followDistance = 0.5f;
    public Rigidbody2D enemyRb;
    private Transform playerTransform;
    public PlayerMovement playerMovement;
    public GameManager gameManagerScript;
    public Audio audioScript;
    private PlayerBody PlayerBodyScript = null;
    public float contactTimeThreshold = 3.0f;
    [SerializeField] public float contactTimer = 0.0f;
    public bool isInContact = false;
    public float force;

    public float knockbackForce = 5.0f; // Adjust this as needed

    private bool isBeingKnockedBack = false;

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

        if (isInContact)
        {
            contactTimer += Time.deltaTime;

            if (contactTimer >= contactTimeThreshold - 1.5f)
            {
                animator.SetBool("isAttacking", true);
            }
            if (contactTimer >= contactTimeThreshold - 0.1f)
            {
                animator.SetBool("isAttacking", false);
                animator.SetTrigger("Attack");
            }

            if (contactTimer >= contactTimeThreshold)
            {
                resetTimer();
                audioScript.PlayerHit();
                gameManagerScript.LoseHealth();
                ApplyKnockbackToPlayer();
                return;   
            }
        }
        else
        {
            resetTimer();
            animator.SetBool("isAttacking", false);
        }
        
        if (!isBeingKnockedBack) // Check if the enemy is not being knocked back
        {
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
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    
                }
                else
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    
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

    private IEnumerator Knockback(Vector2 direction)
    {
        isBeingKnockedBack = true;
        enemyRb.velocity = direction * knockbackForce;

        yield return new WaitForSeconds(0.5f); // Adjust this value as needed

        isBeingKnockedBack = false;
    }

    public void ApplyKnockbackToEnemy(Vector2 direction)
    {
        if (!isBeingKnockedBack)
        {
            StartCoroutine(Knockback(direction));
        }
    }
}
