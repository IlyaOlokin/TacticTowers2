using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Tower
{
    [SerializeField] private GameObject laserBim;

    [SerializeField]private float heatCount = 0;
    private float coolTimer;
    private GameObject currentEnemy;
    private GameObject activeLaser;

    [SerializeField] public int maxHeat;
    [SerializeField] public float bonusDamagePerHeat;
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
        if (enemy == null)
        {
            Destroy(activeLaser);
            currentEnemy = null;
            return;
        }
        LootAtTarget(enemy);

        if (enemy != currentEnemy)
        {
            Destroy(activeLaser);
        }
        if (heatCount < maxHeat) heatCount += Time.deltaTime;
        if (shootDelayTimer <= 0)
        {
            if (enemy != currentEnemy)
            {
                activeLaser = Instantiate(laserBim, transform.position, transform.rotation);
                activeLaser.GetComponent<LaserBim>().target = enemy;
                activeLaser.GetComponent<LaserBim>().origin = transform.position;
                currentEnemy = enemy;
            }
            
            
            shootDelayTimer = 1f / attackSpeed;
            coolTimer = coolDelay;
            
            enemy.GetComponent<Enemy>().TakeDamage(Dmg + Mathf.Floor(heatCount) * bonusDamagePerHeat);
        }
    }
}