using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTower : Tower
{
    [SerializeField] private GameObject bullet;
    
    [SerializeField] private float bulletSpeed;
    
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
            shootDelayTimer = 1f / GetAttackSpeed();
            var newBullet =  Instantiate(bullet, transform.position, towerCanon.transform.rotation);
            var bulletComponent = newBullet.GetComponent<Bullet>();
            bulletComponent.Dmg = GetDmg();
            bulletComponent.Speed = bulletSpeed;
            bulletComponent.enemiesToIgnore = enemiesToIgnore;
            bulletComponent.departurePos = transform.position;
            
            AudioManager.Instance.Play("DefaultTowerShot");
        }
    }
}
