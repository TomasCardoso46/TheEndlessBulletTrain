using UnityEngine;
using UnityEngine.UI;

public class DestroyOnParryZoneContact : MonoBehaviour
{
    [SerializeField]
    private float misfortune;
    public PlayerBody PlayerBodyScript;
    public int health = 3; // Player health
    public Image Misfortune;

    public void Update()
    {

        if (misfortune == 0)
        {
            Misfortune.fillAmount = 0f;
        }
        if (misfortune == 1)
        {
            Misfortune.fillAmount = 0.33f;
        }
        if (misfortune == 2)
        {
            Misfortune.fillAmount = 0.66f;
        }
        if (misfortune == 3)
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
        if (PlayerBodyScript == null)
        {
            Debug.LogError("PlayerBodyScript is not assigned!");
            return;
        }


        // If the object collides with something tagged "ParryZone," destroy this GameObject
        if (other.CompareTag("ParryZone") && PlayerBodyScript.contactTimer >= 1.5f)
        {
            Debug.Log("Contact with ParryZone detected.");
            PlayerBodyScript.contactTimer = 0;
            misfortune += 1;
            
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
