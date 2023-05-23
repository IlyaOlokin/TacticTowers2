using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameField : MonoBehaviour
{
    [SerializeField] private Material material;
    
    [SerializeField] private SpriteRenderer sr;
    private DamageZoneBox damageZoneBox;
    private float totalDuration;
    
    private static readonly int fade = Shader.PropertyToID("_Fade");

    private void Start()
    {
        //sr = GetComponent<SpriteRenderer>();
        sr.material = new Material(material);
        damageZoneBox = GetComponent<DamageZoneBox>();
        totalDuration = damageZoneBox.duration;
        StartCoroutine("Burn", totalDuration * 0.8f);
        sr.material.SetFloat(fade, 1);
    }

    private IEnumerator Burn(float delay)
    {
        yield return new WaitForSeconds(delay);
        var fadeDuration = totalDuration * 0.2f;
        for (float alpha = fadeDuration; alpha >= 0; alpha -= Time.deltaTime)
        {
            sr.material.SetFloat(fade, alpha / fadeDuration);
            yield return null;
        }
        
    }
}
