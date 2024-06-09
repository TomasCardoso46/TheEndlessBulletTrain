using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakuzaSpawner : MonoBehaviour
{
    public GameObject SpawnPoint1;
    
    public Transform objectToSpawn;
    private GameObject SpawnPoint;
    [SerializeField]
    private int numberOfEnemies = 0;
    private bool startSpawn = false;
    private bool canSpawn = true;
    private bool leftSpawner = false;
    // Update is called once per frame
    void Start()
    {
        SpawnPoint = SpawnPoint1;
    }
    void Update()
    {
        if (numberOfEnemies <1 && startSpawn == true && canSpawn == true)
        {
            StartCoroutine(WaitForEnemies());
            SpawnObject();
            canSpawn = false;
            
        }
    }
    public void SpawnObject()
    {

        objectToSpawn.transform.position = SpawnPoint.transform.position;
        numberOfEnemies++;
        
    }
    IEnumerator WaitForEnemies()
    {
        yield return new WaitForSeconds(1.5f);
        canSpawn = true;
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            startSpawn = true;
        }
    }
    


}
