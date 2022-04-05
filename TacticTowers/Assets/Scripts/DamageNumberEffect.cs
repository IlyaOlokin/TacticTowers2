using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DamageNumberEffect : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float effectDuration;
    [SerializeField] private float upscaleDuration;
    [SerializeField] private float scaleMultiplier;
    private float timer;
    private Vector3 targetScale;
    private Vector3 startScale;
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        Destroy(gameObject, effectDuration);
        startScale = transform.localScale;
        targetScale = transform.localScale * scaleMultiplier;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer <= upscaleDuration)
        {
            transform.localScale = Vector2.Lerp(startScale, targetScale, timer / upscaleDuration);
        }
        else
        {
            transform.localScale = Vector2.Lerp(targetScale, startScale,
                (timer - upscaleDuration) / (effectDuration - upscaleDuration));
        }
    }

    public void WriteDamage(float dmg)
    {
        text.text = dmg.ToString();
    }
}
