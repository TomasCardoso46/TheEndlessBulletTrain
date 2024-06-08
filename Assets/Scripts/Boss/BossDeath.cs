using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossDeath : MonoBehaviour
{
    [SerializeField]
    public float misfortune;
    public Boss bossscript;
    
    public Image Misfortune;

    private GrabObject grabScript = null;


    private void UpdateUI()
    {
        //Troquem pra switch case
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
                ApplyKnockback(other);
                misfortune += 2;
                grabScript.grabIsFalse();
                Debug.Log("levaste 2 dano");
                UpdateUI();
                bossscript.resetTimer();
            }
            else if (grabScript.hasObject == false)
            {
                ApplyKnockback(other);
                misfortune += 1;
                Debug.Log("levaste 1 dano");
                UpdateUI();
                bossscript.resetTimer();
            }

            UpdateUI();
        }
        else if (other.CompareTag("Throw"))
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

