using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Triggered with: {other.name}"); // Log which object is triggering the event

        // If the object collides with something tagged "ParryZone," destroy this GameObject
        if (other.CompareTag("ParryZone"))
        {
            Debug.Log("Contact with ParryZone detected.");
            speed = -15f;
        }
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No ParryZone tag detected.");
        }
        
    }
}
