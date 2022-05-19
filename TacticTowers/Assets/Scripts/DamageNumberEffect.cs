using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DamageNumberEffect : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float effectDuration;
    [SerializeField] private float upscaleDuration;
    [SerializeField] private float scaleMultiplier;
    private float timer;
    private Vector3 targetScale;
    private Vector3 startScale;

    [SerializeField] private float posSpreading;
    
    [Header("Right Side")]
    [SerializeField] private List<InnerColor> colors = new List<InnerColor>();

    void Start()
    {
        transform.position += new Vector3(Random.Range(-posSpreading, posSpreading), Random.Range(-posSpreading, posSpreading));
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
        GetInnerColor(dmg);
        if (dmg < 1)
        {
            text.text = "1";
            return;
        }
        
        text.text = (Math.Round(dmg)).ToString();
    }

    private void GetInnerColor(float dmg)
    {
        for (int i = 0; i < colors.Count; i++)
        {
            if (dmg > colors[i].bottomBorder)
            {
                text.color = colors[i].color;
            }
            else
            {
                break;
            }
        }
    }
}

[Serializable]
public class InnerColor
{
    public int bottomBorder;
    public Color color;
}