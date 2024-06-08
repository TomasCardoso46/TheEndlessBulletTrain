/*using UnityEngine;
using UnityEngine.UI;

public class DestroyOnParryZoneContact : MonoBehaviour
{
    [SerializeField] private float misfortune;
    public FollowPlayer CTRScript;
    public Image Misfortune;

    private GrabObject[] grabScripts; // Changed to an array to support multiple GrabObject scripts

    private void Start()
    {
        grabScripts = FindObjectsOfType<GrabObject>(); // Find all GrabObject scripts in the scene
    }

    private void UpdateUI()
    {
        switch (misfortune)
        {
            case 0:
                Misfortune.fillAmount = 0f;
                break;
            case 1:
                Misfortune.fillAmount = 0.50f;
                break;
            case 2:
                Misfortune.fillAmount = 1f;
                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
        Debug.Log($"Triggered with: {other.name}"); // Log which object is triggering the event
        Debug.Log($"Other object's tag: {other.tag}");

        if (other.CompareTag("ParryZone") && CTRScript.contactTimer >= 1.5f)
        {
            int damage = 0;
            foreach (var grabScript in grabScripts)
            {
                if (grabScript.HasObject)
                {
                    damage += 2;
                    grabScript.grabIsFalse();
                    Debug.Log("levaste 2 dano");
                }
                else
                {
                    damage += 1;
                    Debug.Log("levaste 1 dano");
                }
            }

            CTRScript.ApplyKnockbackToEnemy(knockbackDirection);
            misfortune += damage;
            UpdateUI();
            CTRScript.resetTimer();
        }
        else if (other.CompareTag("Mala") || other.CompareTag("Extintor"))
        {
            CTRScript.ApplyKnockbackToEnemy(knockbackDirection);
            misfortune += 1;
            UpdateUI();
        }
    }
}
*/