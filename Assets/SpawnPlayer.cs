using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();
    }
    public void SpawnObject()
    {

        Instantiate(objectToSpawn, SpawnPoint.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
