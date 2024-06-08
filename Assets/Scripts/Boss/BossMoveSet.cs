using UnityEngine;

public class BossMoveSet : MonoBehaviour
{
    public float chargeSpeed = 5.0f; // Speed for the charged attack
    private Transform playerTransform;
    private Animator animator;
    private bool isCharging = false; // Flag to indicate if the boss is performing a charged attack
    private PlayerBody PlayerBodyScript = null;
    private Parry playerParryScript;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            PlayerBodyScript = playerObject.GetComponent<PlayerBody>();
            playerParryScript = playerObject.GetComponent<Parry>();
        }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isCharging)
        {
            PerformChargedAttack();
        }
    }

    public void TryInitiateChargedAttack(float chargeDistance)
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > chargeDistance)
        {
            isCharging = true;
            Debug.Log("Initiating charged attack!");
        }
    }

    private void PerformChargedAttack()
    {
        if (playerTransform == null) return;

        Vector3 direction = playerTransform.position - transform.position;
        direction.Normalize();
        transform.position += direction * chargeSpeed * Time.deltaTime;

        if (animator != null)
        {
            animator.SetBool("IsCharging", true); // Set an animation flag for charging if needed
        }

        Debug.Log("Performing charged attack!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isCharging)
        {
            if (playerParryScript != null && playerParryScript.IsParrying())
            {
                Debug.Log("Player parried the charged attack!");
                // Nullify the damage and stop the charge
                isCharging = false;
                if (animator != null)
                {
                    animator.SetBool("IsCharging", false);
                }
            }
            else if (PlayerBodyScript != null)
            {
                PlayerBodyScript.loseHealth(); // Damage the player on contact during charged attack
                isCharging = false; // Reset charging state
                Debug.Log("Charged attack hit the player!");
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
            isCharging = false;
            if (animator != null)
            {
                animator.SetBool("IsCharging", false);
            }
        }
    }
}
