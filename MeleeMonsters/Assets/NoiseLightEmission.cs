using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class NoiseLightEmission : MonoBehaviour
{
    public float force = 2;

    public List<Light2D> lights = new List<Light2D>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            Light2D light = lights[i];
            light.intensity = calculateEmission(Time.time, i);
        }
    }

    float calculateEmission(float x,float y)
    {
        float sample = Mathf.PerlinNoise(x, y);
        return sample*force;
    }
}
