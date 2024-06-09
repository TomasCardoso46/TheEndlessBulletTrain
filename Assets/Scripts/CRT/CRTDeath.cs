using UnityEngine;
using UnityEngine.UI;

public class DestroyOnParryZoneContact : MonoBehaviour
{
    [SerializeField] private float misfortune;
    public FollowPlayer CTRScript;
    public Image MisfortuneLeft;
    public Image MisfortuneRight;
    public Audio audioScript;

    private GrabObject grabScript;

    private void Start()
    {
        grabScript = FindObjectOfType<GrabObject>();
        if (grabScript == null)
        {
            Debug.LogError("GrabObject script not found in the scene.");
        }
    }

    private void UpdateUI()
    {
        MisfortuneLeft.fillAmount = misfortune / 4f; // Assuming max misfortune is 2 for full fillAmount
        MisfortuneRight.fillAmount = misfortune / 4f; // Assuming max misfortune is 2 for full fillAmount

        if (misfortune >= 4)
        {
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
            if (grabScript != null)
            {
                if (grabScript.hasExtintor)
                {
                    audioScript.Parry();
                    misfortune += 2;
                    grabScript.DoesntHaveExtintor();
                    Debug.Log("levaste 2 dano");
                }
                else if (grabScript.hasMala)
                {
                    audioScript.Parry();
                    misfortune += 2;
                    grabScript.DoesntHaveMala();
                    Debug.Log("levaste 2 dano");
                }
                else
                {
                    audioScript.Parry();
                    misfortune += 1; // Take 1 misfortune if no item is held
                    Debug.Log("levaste 1 dano");
                }
            }
            else
            {
                audioScript.Parry();
                misfortune += 1; // Take 1 misfortune if grabScript is not found
                Debug.Log("levaste 1 dano");
            }
            CTRScript.ApplyKnockbackToEnemy(knockbackDirection);
            UpdateUI();
            CTRScript.resetTimer();
        }
        else if (other.CompareTag("ThrowableMala") || other.CompareTag("ThrowableExtintor"))
        {
            misfortune += 1;
            Debug.Log("LEVASTE DANO");
            UpdateUI();
        }
    }
}
