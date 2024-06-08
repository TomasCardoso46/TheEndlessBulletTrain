using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnParryZoneContact : MonoBehaviour
{
    [SerializeField]
    public float misfortune;
    public FollowPlayer CTRScript;
    
    public Image Misfortune;

    private GrabObject grabScript = null;

    private void UpdateUI()
    {
        // Update the UI based on the misfortune value
        // Troquem pra switch case
        if (misfortune == 0)
        {
            Misfortune.fillAmount = 0f;
        }
        else if (misfortune == 1)
        {
            Misfortune.fillAmount = 0.50f;
        }
        else if (misfortune == 2)
        {
            Misfortune.fillAmount = 1f;
            Destroy(gameObject);
        }

        
    }

    // Call this method to apply knockback to the enemy
    private void ApplyEnemyKnockback()
    {
        // Find the enemy GameObject with the FollowPlayer script attached
        GameObject enemyObject = GameObject.FindGameObjectWithTag("CRT");

        if (enemyObject != null)
        {
            // Get the FollowPlayer component from the enemy GameObject
            FollowPlayer followPlayer = enemyObject.GetComponent<FollowPlayer>();

            if (followPlayer != null)
            {
                // Calculate knockback force (example values)
                Vector2 knockbackDirection = (enemyObject.transform.position - transform.position).normalized;
                Vector2 knockbackForce = knockbackDirection * 5f; // Adjust as needed

                // Call the ApplyKnockback method on the FollowPlayer component
                followPlayer.ApplyKnockback(knockbackForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered with: {other.name}"); // Log which object is triggering the event
        Debug.Log($"Other object's tag: {other.tag}");

        if (other.CompareTag("ParryZone") && CTRScript.contactTimer >= 1.5f)
        {
            if (grabScript == null)
            {
                grabScript = FindObjectOfType<GrabObject>();
                if (grabScript == null)
                {
                    return;
                }
            }

            if (grabScript.hasObject == true)
            {
                ApplyEnemyKnockback();
                misfortune += 2;
                grabScript.grabIsFalse();
                Debug.Log("levaste 2 dano");
                UpdateUI();
                CTRScript.resetTimer();
            }
            else if (grabScript.hasObject == false)
            {
                ApplyEnemyKnockback();
                misfortune += 1;
                Debug.Log("levaste 1 dano");
                UpdateUI();
                CTRScript.resetTimer();
            }

            UpdateUI();
        }
        else if (other.CompareTag("Mala"))
        {
            ApplyEnemyKnockback();
            misfortune += 1;
            UpdateUI();
        }
        else if (other.CompareTag("Extintor"))
        {
            ApplyEnemyKnockback();
            misfortune += 1;
            UpdateUI();
        }
        else
        {
            Debug.Log("No ParryZone tag detected or contactTimer is too low.");
        }
    }
}
///