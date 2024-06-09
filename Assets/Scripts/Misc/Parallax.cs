using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    public float speed; // New variable for automatic movement speed

    private Transform[] layers;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        // Create an array of three layers
        layers = new Transform[3];
        layers[0] = transform;
        for (int i = 1; i < 3; i++)
        {
            GameObject layer = Instantiate(gameObject, new Vector3(startpos + length * i, transform.position.y, transform.position.z), Quaternion.identity);
            layer.transform.parent = transform.parent;
            layers[i] = layer.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        // Move the sprites to the left over time
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].position = new Vector3(layers[i].position.x - speed * Time.deltaTime, layers[i].position.y, layers[i].position.z);
        }

        // Reposition the layers when necessary
        if (temp > layers[1].position.x + length / 2)
        {
            RepositionLayers(1);
        }
        else if (temp < layers[1].position.x - length / 2)
        {
            RepositionLayers(-1);
        }
    }

    void RepositionLayers(int direction)
    {
        if (direction == 1)
        {
            Transform leftmost = layers[0];
            for (int i = 0; i < layers.Length - 1; i++)
            {
                layers[i] = layers[i + 1];
            }
            layers[layers.Length - 1] = leftmost;
            leftmost.position = new Vector3(layers[layers.Length - 2].position.x + length, leftmost.position.y, leftmost.position.z);
        }
        else if (direction == -1)
        {
            Transform rightmost = layers[layers.Length - 1];
            for (int i = layers.Length - 1; i > 0; i--)
            {
                layers[i] = layers[i - 1];
            }
            layers[0] = rightmost;
            rightmost.position = new Vector3(layers[1].position.x - length, rightmost.position.y, rightmost.position.z);
        }
    }
}
