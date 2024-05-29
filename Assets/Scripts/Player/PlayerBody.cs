using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBody : MonoBehaviour
{
    public int health = 3; // Player health
    public Image healthBar;
    public bool isInContact = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if (health <= 0)
        {
            healthBar.fillAmount = 0f;
            DestroyPlayer();
        }
        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Increase strikes in the GameManager
            health--;

            // If strikes reach 3, destroy the object
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