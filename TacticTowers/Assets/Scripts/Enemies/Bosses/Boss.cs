using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] protected Sprite icon;
    [SerializeField] protected float maxHp;
    
    //[NonSerialized]public float hp;
    //[SerializeField] protected Enemy enemyComp;
    //protected bool isDead;

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
}
