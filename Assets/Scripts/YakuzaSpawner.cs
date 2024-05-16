using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakuzaSpawner : MonoBehaviour
{
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject objectToSpawn;
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
        if (numberOfEnemies <=6 && startSpawn == true && canSpawn == true)
        {
            StartCoroutine(WaitForEnemies());
            SpawnObject();
            canSpawn = false;
            SwitchSpawnPoint();
        }
    }
    public void SpawnObject()
    {

        Instantiate(objectToSpawn, SpawnPoint.transform.position, Quaternion.identity);
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
    void SwitchSpawnPoint()
    {
        if (leftSpawner)
        {
            SpawnPoint = SpawnPoint2;
            leftSpawner = false;

        }
        else
        {
            SpawnPoint = SpawnPoint1;
            leftSpawner = true;
        }
    }


}
