using UnityEngine;

public class Parry : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab or object to spawn
    public float leftOffset = 0.1f; // Offset for spawning to the left
    public Transform playerTransform; // The Player's Transform, set via the Inspector
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // Check if the "F" key is pressed and the Player's Transform is set
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Parry");
            // Calculate the spawn position with a slight offset to the left of the Player
            Vector3 spawnPosition = playerTransform.position;
            spawnPosition.x -= leftOffset;

            // Instantiate the object at the calculated position
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        }
    }
}