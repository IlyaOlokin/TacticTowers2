using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaLightning : MonoBehaviour
{
    public bool needSound;
    private AudioSource audioSrc;
    
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        if (needSound) audioSrc.PlayOneShot(audioSrc.clip);
    }
}
