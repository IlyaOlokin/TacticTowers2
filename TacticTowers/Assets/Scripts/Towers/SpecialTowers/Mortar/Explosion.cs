using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 radius;
    private float scaleSpeed;
    [SerializeField] private float explosionDuration;
    private float timer;
    void Start()
    {
        radius = transform.localScale;
        transform.localScale = radius * 0.8f;
        Destroy(gameObject, explosionDuration);
        scaleSpeed = radius.x ;
        timer = explosionDuration;
        AudioManager.Instance.Play("MortarExplosion");
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > explosionDuration * 0.8f)
        {
            var scale = transform.localScale;
            transform.localScale = Vector3.MoveTowards(scale, radius, scaleSpeed * Time.deltaTime);
        }
        else
        {
            transform.localScale =
                Vector3.MoveTowards(transform.localScale, Vector3.zero, scaleSpeed * 1.5f * Time.deltaTime);
        }
    }
}
