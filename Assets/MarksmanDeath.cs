using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksmanDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);   
        }
    }
}
