using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Increase strikes in the GameManager
            GameManager.instance.health--;

            // If strikes reach 3, destroy the object
            if (Player.instance.health <= 0)
            {
                DestroyPlayer();
            }
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
