using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    private Boss boss;
    [SerializeField] private Slider slider;
    [SerializeField] private Image bossIcon;
    
    private Animator anim;
    
    private bool isEnabling;
    private bool isBossAlive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.Play("EnableHpBar");
        isEnabling = true;
        isBossAlive = true;
        slider.value = 0;
    }

    void Update()
    {
        if (isEnabling)
        {
            Enable();
            return;
        }

        if (isBossAlive)
        {
            if (slider.maxValue < boss.GetHp())
                slider.maxValue = boss.GetHp();
            UpdateSlider();
        }
        
        if (boss == null) 
            DisableThis();
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
        slider.value = boss.GetHp();
        if (boss.GetHp() <= 0)
        {
            DisableThis();
        }
    }

    private void DisableThis()
    {
        isBossAlive = false;
        anim.Play("DisableHpBar");
        StartCoroutine("DisableDelay",  1.34f);
    }

    private IEnumerator DisableDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    } 

    public void InitializeBoss(Boss boss)
    {
        this.boss = boss;
        slider.maxValue = Math.Max(this.boss.GetHp(), this.boss.GetMaxHp());
        /*slider.maxValue = this.boss.GetHp() > this.boss.GetMaxHp() 
                                ? this.boss.GetHp()
                                : this.boss.GetMaxHp();*/
        bossIcon.sprite = boss.transform.GetComponent<Boss>().GetIcon();
    }
}
