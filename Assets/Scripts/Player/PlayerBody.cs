using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBody : MonoBehaviour
{
    public int health = 3; // Player health
    public Image healthBar;
    private bool isInContact = false;
    public float contactTimeThreshold = 3.0f;
    private float contactTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInContact)
        {

            contactTimer += Time.deltaTime;


            if (contactTimer >= contactTimeThreshold)
            {
                health -= 1;
                contactTimer = 0.0f;
                return;
            }
            if (health <= 0)
            {
                DestroyPlayer();
            }

        }
        else
        {

            contactTimer = 0.0f;
        }
        if (health == 3)
        {
            healthBar.fillAmount = 1f;
        }
        if (health == 2)
        {
            healthBar.fillAmount = 0.66f;
        }
        if (health == 1)
        {
            healthBar.fillAmount = 0.33f;
        }
        if (health == 0)
        {
            healthBar.fillAmount = 0f;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CRT"))
        {
            isInContact = true;
            Debug.Log("Está em contacto");
        }
        if (other.CompareTag("Bullet"))
        {
            // Increase strikes in the GameManager
            health--;

            // If strikes reach 3, destroy the object
            if (health <= 0)
            {
                DestroyPlayer();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CRT"))
        {
            isInContact = false;
            Debug.Log("Não está em contacto");
        }
    }
    void DestroyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Object.Destroy(player);
        }
    }


}
