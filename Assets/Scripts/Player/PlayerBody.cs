using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBody : MonoBehaviour
{
    public int health = 3; // Player health
    public Image healthBar;
    [SerializeField]
    public bool isInContact = false;
    


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        //Troquem pra switch case
        if (health == 3)
        {
            healthBar.fillAmount = 1f;
        }
        else if (health == 2)
        {
            healthBar.fillAmount = 0.66f;
        }
        else if (health == 1)
        {
            healthBar.fillAmount = 0.33f;
        }
        else if (health <= 0)
        {
            healthBar.fillAmount = 0f;
            DestroyPlayer();
        }
        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Increase strikes in the GameManager
            health--;
            Update();

            // If strikes reach 3, destroy the object
        }
    }
    private void DestroyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
            Object.Destroy(player);     
    }
}