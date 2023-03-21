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
    [SerializeField] private float lifeTime;
    [SerializeField] private float effectDuration;
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float moveUpDistance;
    [SerializeField] private float lerpSpeed;
    private float timer;
    private Vector3 targetScale;
    private Vector3 startScale;
    private Vector3 startPos;
    private Vector3 targetPos;

    [SerializeField] private float posSpreading;
    
    [SerializeField] private List<InnerColor> colors = new List<InnerColor>();

    void Start()
    {
        transform.position += new Vector3(Random.Range(-posSpreading, posSpreading),
            Random.Range(-posSpreading, posSpreading));
        GetComponent<Canvas>().worldCamera = Camera.main;
        Destroy(gameObject, lifeTime);
        startScale = transform.localScale;
        targetScale = startScale * scaleMultiplier;
        startPos = transform.position;
        targetPos = startPos + new Vector3(0, moveUpDistance);

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer <= effectDuration)
        {
            transform.localScale = Vector2.Lerp(startScale, targetScale, timer / effectDuration);
            transform.position = Vector2.Lerp(transform.position, targetPos, lerpSpeed);
        }
        else
        {
            transform.localScale = Vector2.Lerp(targetScale, startScale,
                (timer - effectDuration) / (lifeTime - effectDuration));
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