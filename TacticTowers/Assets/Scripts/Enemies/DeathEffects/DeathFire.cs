using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DeathFire : IDeathEffect
{
    private static readonly int Fade = Shader.PropertyToID("_Fade");
    private const float FadeDuration = 1.2f;

    public void PlayEffect(GameObject source, Vector3 killerPos)
    {
        source.GetComponent<SpriteRenderer>().material = EnemyVFXManager.Instance.GetEffect("DeathFire").material;
        
        source.GetComponent<Collider2D>().enabled = false;
        foreach (var component in source.transform.GetComponentsInChildren(typeof(Collider2D)))
        {
            var collider = (Collider2D)component;
            collider.enabled = false;
        }
        
        //new MonoBehaviour().StartCoroutine("Burn", source.GetComponent<SpriteRenderer>().material);
    }

    private IEnumerator Burn(Material material)
    {
        for (var alpha = FadeDuration; alpha >= 0; alpha -= Time.deltaTime)
        {
            material.SetFloat(Fade, alpha / FadeDuration);
            yield return null;
        }
    }
}