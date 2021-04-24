using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Parallaxe : MonoBehaviour
{

    private Camera cam;
    private float x;

    public List<GameObject> layers = new List<GameObject>();
    public List<float> depths = new List<float>();

    private Vector3 velocity;

    public float smoothTime;
    public float factor;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        x = cam.transform.position.x;
        UpdateParallaxe();
    }

    void UpdateParallaxe()
    {
        for (int i = 0; i < layers.Count; i++)
        {
            GameObject layer = layers[i];
            float depth = depths[i];

            //float oldX = layer.transform.position.x;
            float newX = (-x * factor) / (depth);

            Vector3 newPos = new Vector3(newX, layer.transform.position.y, layer.transform.position.z);
            layer.transform.position = Vector3.SmoothDamp(layer.transform.position, newPos, ref velocity, smoothTime);
        }
    }
}
