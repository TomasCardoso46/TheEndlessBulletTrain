using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public int strikes = 0; // Number of strikes
    public int health = 3;
    public int gualter = 0;
    public SpriteRenderer playerSpriteRenderer;
    public GameObject playerPrefab;
    public PlayerBody playerscript;
    void Awake()
    {
        health -= gualter;
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
    }
    public void LoseHealth()
    {
        health -= 1;
        gualter ++;
        health = Mathf.Clamp(health, 0, 3);  // Ensure health does not go below 0
        
    }
}
