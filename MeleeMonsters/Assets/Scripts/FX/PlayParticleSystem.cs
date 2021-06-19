﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleSystem : MonoBehaviour
{
    public ParticleSystem particle;

    void OnEnable()
    {
        if (particle)
        {
            if (!particle.isPlaying)
            {
                particle.Play();
            }
        }
    }

    void OnDisable()
    {
        if (particle)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
            }
        }
    }
}
