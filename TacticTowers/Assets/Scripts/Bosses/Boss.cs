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
    
    protected virtual void UpdateHp()
    {
        hp = enemyComp.hp;
    } 
}
