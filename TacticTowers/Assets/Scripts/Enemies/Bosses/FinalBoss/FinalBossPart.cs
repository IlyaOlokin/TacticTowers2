using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPart : MonoBehaviour
{
    private Enemy enemyComp;
    private Boss bossAbility;
    [SerializeField] private float bossPartHp;

    private void Start()
    {
        enemyComp = GetComponent<Enemy>();
        bossAbility = GetComponent<Boss>();
    }

    private void Update()
    {
        if (bossPartHp + enemyComp.GetHp() <= 0)
        {
            Destroy(bossAbility);
            Destroy(this);
        }
    }
}
