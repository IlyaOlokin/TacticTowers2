using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class DeathFire : IDeathEffect
{
    private static readonly int Fade = Shader.PropertyToID("_Fade");
    private const float FadeDuration = 1.2f;

    public void PlayEffect(GameObject source, Vector3 killerPos)
    {
        source.GetComponent<Collider2D>().enabled = false;
        foreach (var component in source.transform.GetComponentsInChildren(typeof(Collider2D)))
        {
            var collider = (Collider2D)component;
            collider.enabled = false;
        }

        var newImposter = Object.Instantiate(EnemyVFXManager.Instance.GetEffect("DeathFire").effect,
            source.transform.position, source.transform.rotation);
        newImposter.transform.localScale = source.transform.localScale;
        newImposter.GetComponent<SpriteRenderer>().sprite = source.GetComponent<SpriteRenderer>().sprite;
        newImposter.GetComponent<DestroyThis>().StartCoroutine(Burn(newImposter.GetComponent<SpriteRenderer>().material));
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