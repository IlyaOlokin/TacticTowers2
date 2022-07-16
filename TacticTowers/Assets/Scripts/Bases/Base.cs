using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    public float maxHp;
    [SerializeField] private Canvas canvas;
    [NonSerialized] public float hp;
    
    public Sprite baseImage;

    private void Awake()
    {
        maxHp *= Technologies.BaseHpMultiplier;
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
        hp = maxHp;
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }
    
    //Temp
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ExecuteBaseActiveAbility();
        }
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        UpdateHpBar();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHpBar()
    {
        hpSlider.value = hp;
    }

    public void ExecuteBasePassiveEffect()
    {
        GetComponent<BasePassive>().ExecutePassiveEffect();
    }

    public void ExecuteBaseActiveAbility()
    {
        GetComponent<BaseActive>().ExecuteActiveAbility();
    }

    public float GetHp() => hp;
    public float GetMaxHp() => maxHp;
}
