using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private float maxHp;
    private float hp;

    private void Start()
    {
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
}
