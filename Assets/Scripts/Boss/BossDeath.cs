using UnityEngine;
using UnityEngine.UI;

public class BossDeath : MonoBehaviour
{
    [SerializeField] private float misfortune;
    public Boss bossscript;
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
                Misfortune.fillAmount = 0.20f;
                break;
            case 2:
                Misfortune.fillAmount = 0.40f;
                break;
            case 3:
                Misfortune.fillAmount = 0.60f;
                break;
            case 4:
                Misfortune.fillAmount = 0.80f;
                break;
            case 5:
                Misfortune.fillAmount = 1f;
                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered with: {other.name}"); // Log which object is triggering the event
        Debug.Log($"Other object's tag: {other.tag}");

        if (other.CompareTag("ParryZone") && bossscript.contactTimer >= 1.5f)
        {
            int damage = 0;
            foreach (var grabScript in grabScripts)
            {
                if (grabScript.hasExtintor)
                {
                    damage += 2;
                    grabScript.DoesntHaveExtintor();
                    Debug.Log("levaste 2 dano");
                }
                else if (grabScript.hasMala)
                {
                    damage += 2;
                    grabScript.DoesntHaveMala();
                    Debug.Log("levaste 2 dano");
                }
                else
                {
                    damage += 1;
                    Debug.Log($"levaste 1 dano");
                }
            }

            ApplyKnockback(other);
            misfortune += damage;
            UpdateUI();
            bossscript.resetTimer();
        }
        else if (other.CompareTag("ThrowableExtintor"))
        {
            ApplyKnockback(other);
            misfortune += 1;
            UpdateUI();
        }
        else if (other.CompareTag("ThrowableMala"))
        {
            ApplyKnockback(other);
            misfortune += 1;
            UpdateUI();
        }
        else
        {
            Debug.Log("No ParryZone tag detected or contactTimer is too low.");
        }
    }

    private void ApplyKnockback(Collider2D other)
    {
        Debug.Log("Contact with ParryZone detected.");
        bossscript.resetTimer();
        bossscript.EKBCounter = bossscript.EKBTotalTime;

        if (other.transform.position.x < transform.position.x)
        {
            bossscript.EKnockFromRight = true;
        }
        else
        {
            bossscript.EKnockFromRight = false;
        }
    }
}
