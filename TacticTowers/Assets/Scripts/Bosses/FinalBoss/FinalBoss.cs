using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Boss
{
    [SerializeField] private List<Enemy> bossParts;
    [SerializeField] private List<MonoBehaviour> bossFunctionality;
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

    public void ActivateBossFunctionality()
    {
        foreach (var component in bossFunctionality) component.enabled = true;
    }
    
    public void DeactivateBossFunctionality()
    {
        foreach (var component in bossFunctionality) component.enabled = false;
    }
}
