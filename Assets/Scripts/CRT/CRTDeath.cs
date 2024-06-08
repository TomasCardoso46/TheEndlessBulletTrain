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

    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
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

                CTRScript.ApplyKnockbackToEnemy(knockbackDirection);
                misfortune += 2;
                grabScript.grabIsFalse();
                Debug.Log("levaste 2 dano");
                UpdateUI();
                CTRScript.resetTimer();
            }
            else if (grabScript.hasObject == false)
            {
                CTRScript.ApplyKnockbackToEnemy(knockbackDirection);
                misfortune += 1;
                Debug.Log("levaste 1 dano");
                UpdateUI();
                CTRScript.resetTimer();
            }

            UpdateUI();
        }
        else if (other.CompareTag("Mala"))
        {
            CTRScript.ApplyKnockbackToEnemy(knockbackDirection);
            misfortune += 1;
            UpdateUI();
        }
        else if (other.CompareTag("Extintor"))
        {
            CTRScript.ApplyKnockbackToEnemy(knockbackDirection);
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