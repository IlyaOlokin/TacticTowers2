using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Tower
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private SpriteRenderer heatIndicator;

    private float heatCount = 0;
    private float coolTimer;

    [SerializeField] public int maxHeat;
    [SerializeField] public float maxHeatMultiplier;
    [SerializeField] public float bonusAttackSpeedPerHeat;
    [SerializeField] public float bonusAttackSpeedPerHeatMultiplier;
    [SerializeField] public float coolDelay;
    [SerializeField] public float coolDelayMultiplier;

    [NonSerialized] public bool hasPenetrationUpgrade;

    private GameObject currentEnemy;

    [Header("Penetration Upgrade")] 
    [SerializeField] private float penetrationDamageMultiplier;
    [SerializeField] private int penetrationsCount;
    
    [NonSerialized] public bool hasDamageStackUpgrade;
    
    [Header("Damage Stack Upgrade")] 
    [SerializeField] private float bonusMultiplierForStack;
    [SerializeField] private int maxDamageStacksCount;
    private int damageStacksCount;
    
    [NonSerialized] public bool hasCriticalUpgrade;

    [Header("Critical Upgrade")]
    [SerializeField] private float critChance;

    
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

        BarrelHeat();
    }

    private void BarrelHeat()
    {
        var color = heatIndicator.color;
        color = Color.HSVToRGB(0, heatCount / (maxHeat * maxHeatMultiplier) * 0.6f , 1);
        heatIndicator.color = color;
    }

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;

        if (currentEnemy != enemy) ResetDamageStacksCount();

        currentEnemy = enemy;

        LootAtTarget(enemy.transform.position);

        if (shootDelayTimer <= 0)
        {
            heatCount = Mathf.Ceil(heatCount);
            shootDelayTimer = 1f / (GetAttackSpeed() + heatCount * bonusAttackSpeedPerHeat * bonusAttackSpeedPerHeatMultiplier);
            if (heatCount < GetMaxHeat()) heatCount += 1;
            coolTimer = coolDelay * coolDelayMultiplier;
            var newBullet = Instantiate(bullet, transform.position, towerCanon.transform.rotation);
            var bulletComponent = newBullet.GetComponent<Bullet>();
            
            if (hasCriticalUpgrade && heatCount >= GetMaxHeat() && IsCriticalShot(critChance))
            {
                bulletComponent.ActivateVisualEffect();
                bulletComponent.Dmg = GetDmg() * 2f;
                bulletComponent.isCritical = true;
            }
            else bulletComponent.Dmg = GetDmg();
            
            bulletComponent.Speed = bulletSpeed;
            bulletComponent.enemiesToIgnore = enemiesToIgnore;
            bulletComponent.departurePos = transform.position;
            bulletComponent.hasPenetrationUpgrade = hasPenetrationUpgrade;
            bulletComponent.penetrationDamageMultiplier = penetrationDamageMultiplier;
            bulletComponent.penetrationsCount = penetrationsCount;
            var minigunBulletComponent = newBullet.GetComponent<MinigunBullet>();
            minigunBulletComponent.sender = this;
            minigunBulletComponent.target = enemy;

            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }

    public float GetBonusStackDamageMultiplier() => 1 + bonusMultiplierForStack * damageStacksCount;

    public float GetMaxHeat() => maxHeat * maxHeatMultiplier;

    public void IncrementStacksCount()
    {
        if (damageStacksCount < maxDamageStacksCount) damageStacksCount++;
    }

    public void ResetDamageStacksCount() => damageStacksCount = 0;
}