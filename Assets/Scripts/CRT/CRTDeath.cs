using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnParryZoneContact : MonoBehaviour
{
    [SerializeField]
    public float misfortune;
    public FollowPlayer CTRScript;
    
    public Image Misfortune;
    public GrabObject GrabScript;
    public GameObject FollowPlayer;
    

    private void UpdateUI()
    {
        //Troquem pra switch case
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
        Debug.Log($"Triggered with: {other.name}"); // Log which object is triggering the event
        Debug.Log($"Other object's tag: {other.tag}");

        if (other.CompareTag("ParryZone") && CTRScript.contactTimer >= 1.5f)
        {
            if (GrabScript.hasObject == true)
            {
                ApplyKnockback(other);
                misfortune += 2;
                GrabScript.hasObject = false;
                Debug.Log("levaste 2 dano");
                UpdateUI();
                CTRScript.contactTimer = 0;
            }
            else if (GrabScript.hasObject == false)
            {
                ApplyKnockback(other);
                misfortune += 1;
                Debug.Log("levaste 1 dano");
                UpdateUI();
                CTRScript.contactTimer = 0;
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
        CTRScript.contactTimer = 0;
        CTRScript.EKBCounter = CTRScript.EKBTotalTime;

        if (other.transform.position.x < transform.position.x)
        {
            CTRScript.EKnockFromRight = true;
        }
        else
        {
            CTRScript.EKnockFromRight = false;
        }
    }
}

        

        




