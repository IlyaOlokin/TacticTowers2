using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss")]
    [SerializeField] protected Sprite icon;
    [SerializeField] protected float maxHp;
    protected bool isFunctional = true;

    public float GetMaxHp() => maxHp;
    public Sprite GetIcon() => icon;
    
    protected virtual void UpdateHp()
    {
        if (hp <= 0) 
            BossDeath();
    }

    protected virtual void BossDeath()
    {
        isDead = true;
    }

    public void SetFunctionality(bool isFunctional)
    {
        this.isFunctional = isFunctional;
    }
}
