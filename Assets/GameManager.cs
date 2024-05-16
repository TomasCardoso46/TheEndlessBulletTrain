using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public int health = 0; // Player health
    public int strikes = 0; // Number of strikes
    public SpriteRenderer playerSpriteRenderer;
    public GameObject playerPrefab;

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
            DestroyPlayer();
        }
    }
    void DestroyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Destroy(player);
        }
    }
}
