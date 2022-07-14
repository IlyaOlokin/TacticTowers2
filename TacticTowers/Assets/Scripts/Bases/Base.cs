using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private float maxHp;
    [SerializeField] private Canvas canvas;
    private float hp;

    public string activeDescription;
    public string passiveDescription;
    public Sprite baseImage;

    private void Awake()
    {
        canvas.worldCamera = Camera.main;
        maxHp *= Technologies.BaseHpMultiplier;
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
        hp = maxHp;
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        hpSlider.value = hp;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void ExecuteBaseEffects()
    {
        
    }

    public float GetHp() => hp;
    public float GetMaxHp() => maxHp;
}
