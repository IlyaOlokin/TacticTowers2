using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Sprite icon;
    [NonSerialized]public float hp;
    public float maxHp;
    [SerializeField] protected Enemy enemyComp;
    protected bool isDead;
    
    protected virtual void UpdateHp()
    {
        if (enemyComp.hp <= 0) BossDeath();
        hp = enemyComp.hp;
    }

    protected virtual void BossDeath()
    {
        
    }
}
