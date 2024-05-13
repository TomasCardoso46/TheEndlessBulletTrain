using UnityEngine;

public class LevelSwitch : MonoBehaviour
{
    // Specify the specific location where the player should be teleported
    public Transform teleportLocation;

    // Detect collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the tag "Pica"
        if (collision.CompareTag("Pica"))
        {
            // Find all objects with the tag "Player"
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            // Loop through all found player objects and teleport them
            foreach(GameObject player in players)
            {
                // Teleport the player to the specified location if it exists
                if (teleportLocation != null)
                {
                    // Teleport the player by setting its position
                    player.transform.position = teleportLocation.position;
                }
                else
                {
                    Debug.LogError("Teleport location is not assigned!");
                }
            }
        }
    }
}
