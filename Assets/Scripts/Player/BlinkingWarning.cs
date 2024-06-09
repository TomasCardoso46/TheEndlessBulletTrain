using System.Collections;
using UnityEngine;

public class BlinkingWarning : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float flashInterval = 0.5f; // Interval between flashes in seconds
    public float duration = 3f; // Duration of the blinking effect
    public Transform SpawnPoint;
    public GameObject objectToSpawn;
    public float spawnInterval = 10f; // Interval between spawns in seconds

    private void Start()
    {
        // Start the repeated spawning of the object every 10 seconds
        InvokeRepeating(nameof(SpawnObject), 10f, spawnInterval);
    }

    private IEnumerator BlinkRoutine()
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

        // Ensure the sprite is invisible at the end
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartBlinking();
        }
    }

    public void StartBlinking()
    {
        StartCoroutine(BlinkRoutine());
    }

    public void SpawnObject()
    {
        Instantiate(objectToSpawn, SpawnPoint.position, Quaternion.identity);
        StartBlinking();
    }
}
