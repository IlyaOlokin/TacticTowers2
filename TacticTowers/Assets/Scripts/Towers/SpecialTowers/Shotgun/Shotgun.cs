using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Tower
{
    [SerializeField] private GameObject bullet;
    
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float angleBetweenBullets;
    public int bulletCount;
    
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
            for (var i = 1; i <= bulletCount; i++)
            {
                var towerRot = transform.eulerAngles;
                var bulletAngle = 0f;
            
                if (bulletCount % 2 == 1)
                    bulletAngle = i / 2 * angleBetweenBullets * (i % 2 == 0 ? 1 : -1f);
                else
                    bulletAngle = i / 2 * angleBetweenBullets * (i % 2 == 0 ? 1 : -1f) -
                                  angleBetweenBullets / 2f;

                var newBullet =  Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, bulletAngle + towerRot.z));
                newBullet.GetComponent<Bullet>().Dmg = GetDmg();
                newBullet.GetComponent<Bullet>().Speed = bulletSpeed;
            }
            shootDelayTimer = 1f / GetAttackSpeed();
        }
    }
}