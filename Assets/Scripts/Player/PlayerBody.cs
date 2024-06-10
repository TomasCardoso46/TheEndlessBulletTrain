using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBody : MonoBehaviour
{
    public UIHealth uiheatlh;
    private GameManager gameManagerScript;

    [SerializeField]
    private FollowPlayer CRTScript;

    // Start is called before the first frame update
    void Awake()
    {
        gameManagerScript = GameManager.instance;
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
            gameManagerScript.LoseHealth();
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

    
}
