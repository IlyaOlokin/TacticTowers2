using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Tower
{
    [SerializeField] private GameObject laserBim;

    private float heatCount = 0;
    private float coolTimer;
    private GameObject currentEnemy;
    private GameObject activeLaser;

    [SerializeField] public int maxHeat;
    [SerializeField] public float bonusDamagePerHeat;
    [SerializeField] public float coolDelay;

    [NonSerialized] public bool shooting;
    private DamageType damageType = DamageType.Fire;

    private void Start()
    {
        AudioManager.Instance.lasers.Add(this);
    }

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
            //FindObjectOfType<AudioManager>().Stop("LaserShot");
            shooting = false;
            currentEnemy = null;
            return;
        }
        LootAtTarget(enemy);

        if (enemy != currentEnemy)
        {
            Destroy(activeLaser);
            //FindObjectOfType<AudioManager>().Stop("LaserShot");
            shooting = false;

        }
        if (heatCount < maxHeat) heatCount += Time.deltaTime;
        if (activeLaser != null)  activeLaser.GetComponent<LaserBim>().IncreaseWidth(heatCount);
        if (shootDelayTimer <= 0)
        {
            if (enemy != currentEnemy)
            {
                activeLaser = Instantiate(laserBim, transform.position, towerCanon.transform.rotation);
                activeLaser.GetComponent<LaserBim>().target = enemy;
                activeLaser.GetComponent<LaserBim>().origin = transform.position;
                currentEnemy = enemy;
                //FindObjectOfType<AudioManager>().Play("LaserShot");
                shooting = true;
            }
            
            
            shootDelayTimer = 1f / GetAttackSpeed();
            coolTimer = coolDelay;
            
            enemy.GetComponent<Enemy>().TakeDamage(GetDmg() + Mathf.Floor(heatCount) * bonusDamagePerHeat, damageType);
        }
    }
}