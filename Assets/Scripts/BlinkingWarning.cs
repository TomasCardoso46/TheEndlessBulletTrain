using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingWarning : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float flashInterval = 0.5f; // Interval between flashes in seconds
    public float duration = 3f; // Duration of the blinking effect
    public GameObject objectToSpawn;
    public Vector3 spawnPosition;
    // Call this method to start the blinking effect
    public void StartBlinking()
    {
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        // Make sprite invisible initially
        spriteRenderer.enabled = false;

        // Blinking
        float timer = 0f;
        while (timer < duration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }
        SpawnObject();

        // Ensure the sprite is invisible at the end
        spriteRenderer.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartBlinking();
        }
    }
    public void SpawnObject()
    {
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    
}
