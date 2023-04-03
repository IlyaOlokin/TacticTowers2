using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DamageNumberEffect : MonoBehaviour
{
    [SerializeField] private Transform criticalEffect;
    [SerializeField] private Transform objectToMove;
    [SerializeField] private Text text;
    [SerializeField] private float lifeTime;
    [SerializeField] private float effectDuration;
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float moveDistance;
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
        objectToMove.position += new Vector3(Random.Range(-posSpreading, posSpreading),
            Random.Range(-posSpreading, posSpreading));
        GetComponent<Canvas>().worldCamera = Camera.main;
        Destroy(gameObject, lifeTime);
        startScale = objectToMove.localScale;
        targetScale = startScale * scaleMultiplier;
        startPos = objectToMove.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer <= effectDuration)
        {
            objectToMove.localScale = Vector2.Lerp(startScale, targetScale, timer / effectDuration);
            objectToMove.position = Vector2.Lerp(objectToMove.position, targetPos, lerpSpeed);
        }
        else
        {
            objectToMove.localScale = Vector2.Lerp(targetScale, startScale,
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

    public void InitTargetPos(Vector3 damagerPos, bool isCritical)
    {
        if ((damagerPos - objectToMove.position).magnitude < posSpreading)
        {
            targetPos = objectToMove.position + new Vector3(0, moveDistance);
            return;
        }
        var dir = (objectToMove.position - damagerPos).normalized;
        targetPos = objectToMove.position + dir * moveDistance;

        if (!isCritical) return;
        criticalEffect.gameObject.SetActive(true);
        float angle = Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
        criticalEffect.eulerAngles = new Vector3(0, 0, angle - 90);
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