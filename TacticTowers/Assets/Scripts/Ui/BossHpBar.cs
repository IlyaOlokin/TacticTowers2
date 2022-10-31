using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    private Enemy boss;
    [SerializeField] private Slider slider;
    [SerializeField] private Image bossIcon;
    
    private Animator anim;
    
    private bool isEnabling;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.Play("EnableHpBar");
        isEnabling = true;
        slider.value = 0;
    }

    void Update()
    {
        if (!isEnabling) UpdateSlider();

        Enable();
    }

    private void Enable()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("EnableHpBar"))
            return;
        
        if (slider.value < slider.maxValue)
            slider.value += slider.maxValue / 2f * Time.deltaTime;
        else
            isEnabling = false;
    }
    
    

    private void UpdateSlider()
    {
        slider.value = boss.hp;
        if (boss.hp <= 0)
        {
            DisableThis();
        }
    }

    private void DisableThis()
    {
        
    }

    public void InitializeBoss(Enemy boss)
    {
        this.boss = boss;
        slider.maxValue = this.boss.hp;
        bossIcon.sprite = boss.transform.GetComponent<Boss>().icon;
    }
}
