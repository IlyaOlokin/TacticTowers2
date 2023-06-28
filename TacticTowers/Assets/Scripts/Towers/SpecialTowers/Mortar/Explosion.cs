using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [NonSerialized] public bool needSound;

    private Vector3 radius;
    
    [SerializeField] private float explosionDuration;

    private float scaleMultiplier = 0.75f;
    [SerializeField] private List<GameObject> particleSystems;

    private AudioSource audioSrc;

    private void Start()
    {
        radius = transform.localScale;
        foreach (var ps in particleSystems)
        {
            ps.transform.localScale = radius * scaleMultiplier;
        }
        audioSrc = GetComponent<AudioSource>();
        if (needSound) audioSrc.PlayOneShot(audioSrc.clip);
        Destroy(gameObject, explosionDuration);
        //AudioManager.Instance.Play("MortarExplosion");
    }
}
