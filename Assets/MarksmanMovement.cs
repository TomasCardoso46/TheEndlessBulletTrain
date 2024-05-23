using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MarksmanMovement : MonoBehaviour
{
    public Transform SpawnPoint;
    public float speed = 5.0f;
    public float followDistance = 0.5f;
    private Transform playerTransform;
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float fireRateBom = 0f;
    [SerializeField]
    public float fireRateThreshold = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Vector3 spawnPosition = SpawnPoint.position;

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {

            Vector3 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude;
            if (distance > followDistance)
            {
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;
                fireRateBom = 0;
            }

            if (distance <= followDistance)
            {
                TimeIncrease();

                if (fireRateBom >= fireRateThreshold)
                {
                    ShootPlayer();
                    fireRateBom = 0.0f;
                    return;
                }

            }

        }
        void ShootPlayer()
        {

            Instantiate(bullet, SpawnPoint.position, Quaternion.identity);

        }
        void TimeIncrease()
        {
            fireRateBom += Time.deltaTime;
        }
    }
}