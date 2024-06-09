using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    public float speed; // New variable for automatic movement speed

    private List<Transform> layers;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        // Create a list of layers
        layers = new List<Transform>();
        layers.Add(transform);
        for (int i = 1; i < 3; i++)
        {
            GameObject layer = Instantiate(gameObject, new Vector3(startpos + length * i, transform.position.y, transform.position.z), Quaternion.identity);
            layer.transform.parent = transform.parent;
            layers.Add(layer.transform);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        // Move the sprites to the left over time
        foreach (var layer in layers)
        {
            layer.position = new Vector3(layer.position.x - speed * Time.deltaTime, layer.position.y, layer.position.z);
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
            layers.RemoveAt(0);
            Destroy(leftmost.gameObject);
            GameObject newLayer = Instantiate(gameObject, new Vector3(layers[layers.Count - 1].position.x + length, layers[0].position.y, layers[0].position.z), Quaternion.identity);
            newLayer.transform.parent = transform.parent;
            layers.Add(newLayer.transform);
        }
        else if (direction == -1)
        {
            Transform rightmost = layers[layers.Count - 1];
            layers.RemoveAt(layers.Count - 1);
            Destroy(rightmost.gameObject);
            GameObject newLayer = Instantiate(gameObject, new Vector3(layers[0].position.x - length, layers[0].position.y, layers[0].position.z), Quaternion.identity);
            newLayer.transform.parent = transform.parent;
            layers.Insert(0, newLayer.transform);
        }
    }
}
