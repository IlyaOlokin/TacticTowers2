using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Tower
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    
    private float heatCount = 0;
    private float coolTimer;

    [SerializeField] public int maxHeat;
    [SerializeField] public float bonusAttackSpeedPerHeat;
    [SerializeField] public float coolDelay;

    void Update()
    {
        base.Update();

        if (coolTimer < 0)
        {
            if (heatCount > 0) heatCount -= Time.deltaTime * 2;
            else heatCount = 0;
        }
        else coolTimer -= Time.deltaTime;
    }
    
    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;
        
        LootAtTarget(enemy);
        
        if (shootDelayTimer <= 0)
        {
            heatCount = Mathf.Ceil(heatCount);
            shootDelayTimer = 1f / (attackSpeed + heatCount * bonusAttackSpeedPerHeat);
            if (heatCount < maxHeat) heatCount += 1;
            coolTimer = coolDelay;
            var newBullet =  Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Bullet>().Dmg = Dmg;
            newBullet.GetComponent<Bullet>().Speed = bulletSpeed;
            newBullet.GetComponent<Bullet>().OnCreate();
        }
    }
}
