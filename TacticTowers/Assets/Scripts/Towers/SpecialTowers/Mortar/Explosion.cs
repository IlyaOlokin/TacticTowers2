using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 radius;
    
    [SerializeField] private float explosionDuration;

    private float scaleMultiplier = 0.75f;
    [SerializeField] private List<GameObject> particleSystems;
    void Start()
    {
        radius = transform.localScale;
        foreach (var ps in particleSystems)
        {
            ps.transform.localScale = radius * scaleMultiplier;
        }
        Destroy(gameObject, explosionDuration);
        AudioManager.Instance.Play("MortarExplosion");
    }
}
