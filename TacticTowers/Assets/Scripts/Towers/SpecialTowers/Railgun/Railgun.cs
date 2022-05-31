using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : Tower
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform railStartPos;
    public float dmgMultiplier;
    public float minDmg;
    
    
    void Update()
    {
        base.Update();
    }
    
    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;
        
        LootAtTarget(enemy);
        
        if (shootDelayTimer <= 0)
        {
            
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, towerCanon.transform.up, Mathf.Infinity);
            var newRail = Instantiate(bullet, transform.position, towerCanon.transform.rotation);
            newRail.GetComponent<LineRenderer>().SetPosition(0, railStartPos.position);
            newRail.GetComponent<LineRenderer>().SetPosition(1, transform.position + towerCanon.transform.up * 50);
            float multiplier = 1f;
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                Enemy newEnemy = hit.transform.GetComponent<Enemy>();

                if (newEnemy)
                {
                    if (multiplier < minDmg) multiplier = minDmg;
                    newEnemy.TakeDamage(GetDmg() * multiplier);
                    multiplier *= dmgMultiplier;
                }
            }

            shootDelayTimer = 1f / GetAttackSpeed();
            
            AudioManager.Instance.Play("RailgunShot");
        }
    }
}