using UnityEngine;
using UnityEngine.UI;

public class DestroyOnParryZoneContact : MonoBehaviour
{
    [SerializeField]
    public float misfortune;
    public FollowPlayer CTRScript;
    public Image Misfortune;

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
            DestroyUI();
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered with: {other.name}"); // Log which object is triggering the event

        // Log the tag of the other object
        Debug.Log($"Other object's tag: {other.tag}");

        // Check if PlayerBodyScript is assigned


        // If the object collides with something tagged "ParryZone," destroy this GameObject
        if (other.CompareTag("ParryZone") && CTRScript.contactTimer >= 1.5f)
        {
            Debug.Log("Contact with ParryZone detected.");
            CTRScript.contactTimer = 0;
            misfortune += 1;
            UpdateUI();
        }
        else if (other.CompareTag("Grab"))
        {
            Debug.Log("Contact with ParryZone detected.");
            misfortune += 1;
            UpdateUI();
        }
        else
        {
            Debug.Log("No ParryZone tag detected or contactTimer is too low.");
        }

    }

    void DestroyUI()
    {
        GameObject Misfortune = GameObject.FindGameObjectWithTag("Misfortune");

        if (Misfortune != null)
        {
            Object.Destroy(Misfortune);
        }
    }



}
