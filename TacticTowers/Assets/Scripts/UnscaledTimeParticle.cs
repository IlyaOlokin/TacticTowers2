using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledTimeParticle : MonoBehaviour
{
    void Start()
    {
        var mainModule = GetComponent<ParticleSystem>().main;
        mainModule.useUnscaledTime = true;
    }
    
}
