using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Boss
{
    [SerializeField] private List<Enemy> bossParts;
    void Update()
    {
        UpdateHp();
    }

    protected override void UpdateHp()
    {
        hp = maxHp + GetDamageDone();
        if (hp <= 0) Destroy(gameObject);
    }

    private float GetDamageDone()
    {
        float hp = 0;
        foreach (var bossPart in bossParts)
        {
            hp += bossPart.hp;
        }

        return hp;
    }
}
