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
    [SerializeField] public float maxHeatMultiplier;
    [SerializeField] public float multiplierPerHeatStack;
    [SerializeField] public float multiplierPerHeatStackMultiplier;
    [SerializeField] public float coolDelay;
    [SerializeField] public float coolDelayMultiplier;
    [SerializeField] private ContactFilter2D contactFilter;

    [NonSerialized] public bool shooting;
    private DamageType damageType = DamageType.Fire;

    private void Start() => audioSrc = GetComponent<AudioSource>();

    private new void Update()
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
            audioSrc.Stop();
            shooting = false;
            currentEnemy = null;
            return;
        }
        LootAtTarget(enemy);
        
        if (enemy != currentEnemy)
        {
            Destroy(activeLaser);
            audioSrc.Stop();
            shooting = false;

        }
        if (heatCount < maxHeat * maxHeatMultiplier) heatCount += Time.deltaTime;
        if (activeLaser != null)  activeLaser.GetComponent<LaserBim>().IncreaseWidth(heatCount);
        if (enemy != currentEnemy)
        {
            activeLaser = Instantiate(laserBim, transform.position, towerCanon.transform.rotation);
            activeLaser.GetComponent<LaserBim>().target = enemy;
            activeLaser.GetComponent<LaserBim>().origin = transform.position;
            currentEnemy = enemy;
            audioSrc.Play();
            shooting = true;
        }
        
        if (shootDelayTimer <= 0)
        {
            shootDelayTimer = 1f / GetAttackSpeed();
            coolTimer = coolDelay * coolDelayMultiplier;

            if (CheckWallCollision(transform.position, enemy.transform.position, GetShootDistance(), false) is null)
            {
                if (enemy.GetComponent<Enemy>().TakeDamage(
                    GetDmg() * (1 + Mathf.Floor(heatCount) * multiplierPerHeatStack * multiplierPerHeatStackMultiplier),
                    damageType, transform.position))
                {
                    Destroy(activeLaser);
                    shooting = false;
                }
            }
                
        }
    }
}