using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [NonSerialized] public float burnTime;
    [NonSerialized] public float burnDmg;
    [NonSerialized] public GameObject fire;
    private GameObject newFire;
    private Enemy enemy;
    
    private float dmgDelayTimer;
    private float dmgDelay = 0.5f;
    private DamageType damageType = DamageType.Fire;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        newFire = Instantiate(fire, transform.position, Quaternion.identity, enemy.transform);
    }

    void Update()
    {
        burnTime -= Time.deltaTime;
        if (burnTime <= 0)
        {
            Destroy(newFire.gameObject);
            Destroy(this);
        }
        
        if (dmgDelayTimer > 0) dmgDelayTimer -= Time.deltaTime;
        
        if (dmgDelayTimer <= 0) DealDamage();
    }

    private void DealDamage()
    {
        enemy.TakeDamage(burnDmg, damageType, transform.position);
        dmgDelayTimer = dmgDelay;
    }
}
