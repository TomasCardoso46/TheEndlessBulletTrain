using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBody : MonoBehaviour
{
    public int health = 3; // Player health
    public UIHealth uiheatlh;
    
    [SerializeField]
    private FollowPlayer CRTScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        // Only call UpdateHealthSprite() if necessary to reduce redundant calls
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Decrease health
            LoseHealth();
        }
    }

    private void DestroyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }
    }

    public void LoseHealth()
    {
        health -= 1;
        health = Mathf.Clamp(health, 0, 3);  // Ensure health does not go below 0
        uiheatlh.UpdateHealthSprite();
    }
}
