using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public int health = 3; // Player health
    public int strikes = 0; // Number of strikes
    public SpriteRenderer playerSpriteRenderer;
    public GameObject playerPrefab;
    public Image healthBar;

    void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
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
        else
        {
            healthBar.fillAmount = 0f;
        }
    }
    
}
