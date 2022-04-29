using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Tower
{
    [SerializeField] private GameObject bullet;
    
    [SerializeField] private float bulletSpeed;
    public float explosionRadius;

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
            var newBullet =  Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<MortarProjectile>().Dmg = GetDmg();
            newBullet.GetComponent<MortarProjectile>().Speed = bulletSpeed;
            newBullet.GetComponent<MortarProjectile>().radius = explosionRadius;
            newBullet.GetComponent<MortarProjectile>().targetPos = enemy.transform.position;
            
            shootDelayTimer = 1f / GetAttackSpeed();
        }
    }
}