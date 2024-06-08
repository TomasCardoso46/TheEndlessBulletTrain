using UnityEngine;
using System.Collections;

public class pica : MonoBehaviour
{
    public SpriteRenderer prefabSpriteRenderer; // Reference to the SpriteRenderer of the prefab to blink
    [SerializeField] private float speed = 5f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Move the object to the right
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Increase strikes in the GameManager
            GameManager.instance.strikes++;

            // Start blinking the prefab
            StartCoroutine(BlinkPrefab());

            // Get the Strikes component and update the health sprite
            Strikes strikesComponent = GameObject.FindObjectOfType<Strikes>();
            if (strikesComponent != null)
            {
                strikesComponent.UpdateStrikeSprite(GameManager.instance.strikes);
            }

            // If strikes reach 3, destroy the player object
            if (GameManager.instance.strikes >= 3)
            {
                DestroyPlayer();
            }
        }
    }

    IEnumerator BlinkPrefab()
    {
        // Make the prefab blink for 0.3 seconds
        float blinkDuration = 0.3f;
        float timer = 0f;
        while (timer < blinkDuration)
        {
            prefabSpriteRenderer.enabled = !prefabSpriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f); // Toggle visibility every 0.1 seconds
            timer += 0.1f;
        }

        // Ensure the sprite is visible after blinking
        prefabSpriteRenderer.enabled = true;
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
