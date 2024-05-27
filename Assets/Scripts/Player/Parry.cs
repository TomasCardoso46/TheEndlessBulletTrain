using UnityEngine;
using System.Collections;

public class Parry : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab or object to spawn
    public float leftOffset = 0.1f; // Offset for spawning to the left
    public Transform playerTransform; // The Player's Transform, set via the Inspector
    private Animator animator;
    private bool canParry = true; // Bool to track if parry is available

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the "F" key is pressed, the Player's Transform is set, and parry is available
        if (Input.GetKeyDown(KeyCode.F) && canParry)
        {
            StartCoroutine(ParryAction());
        }
    }

    private IEnumerator ParryAction()
    {
        canParry = false; // Set canParry to false to prevent another parry
        animator.SetTrigger("Parry");

        // Calculate the spawn position with a slight offset to the left of the Player
        Vector3 spawnPosition = playerTransform.position;
       

        // Instantiate the object at the calculated position
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(1f); // Wait for 1 second
        canParry = true; // Reset canParry to true after the cooldown
    }
}
