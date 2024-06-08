using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Vector3 playerDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the direction towards the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            // If player is not found, shoot the bullet to the left by default
            playerDirection = Vector3.left;
        }
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet in the direction of the player
        transform.Translate(playerDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered with: {other.name}"); // Log which object is triggering the event

        // If the object collides with something tagged "ParryZone," destroy this GameObject
        if (other.CompareTag("ParryZone"))
        {
            Debug.Log("Contact with ParryZone detected.");
            speed = -15f;
        }
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No ParryZone tag detected.");
        }

    }
}
