using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Tower
{
    [SerializeField] private GameObject bullet;
    
    [SerializeField] private float bulletSpeed;
    public float explosionRadius;
    public float explosionRadiusMultiplier;

    
    [NonSerialized] public bool hasFlameFieldUpgrade;
    [Header("Flame Field Upgrade")]
    [SerializeField] private float flameFieldDamageMultiplier = 0.1f;
    
    
    [NonSerialized] public bool hasScatterUpgrade;
    [Header("Scatter Upgrade")]
    [SerializeField] private int angleBetweenSubProjectiles = 30;
    [SerializeField] private float subProjectilesDamageMultiplier = 0.2f;
    [SerializeField] private float subProjectilesSpeedMultiplier = 0.5f;
    [SerializeField] private float subProjectilesRadiusMultiplier = 0.5f;
    
    [NonSerialized] public bool hasFireChanceUpgrade;

    [Header("Fire Chance Upgrade")] 
    [SerializeField] private float chanceToSetOnFire;
    [SerializeField] private float burnTime;
    [SerializeField] private float burnDamageMultiplier;


    private void Start() => audioSrc = GetComponent<AudioSource>();
    private new void Update() => base.Update();

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;
        
        LootAtTarget(enemy.transform.position);
        
        if (shootDelayTimer <= 0)
        {
            var newBullet =  Instantiate(bullet, transform.position, towerCanon.transform.rotation);
            MortarProjectile mortarProjectile = newBullet.GetComponent<MortarProjectile>();
            mortarProjectile.dmg = GetDmg();
            mortarProjectile.speed = bulletSpeed;
            mortarProjectile.radius = explosionRadius * explosionRadiusMultiplier;
            mortarProjectile.targetPos = enemy.transform.position;
            mortarProjectile.hasFlameFieldUpgrade = hasFlameFieldUpgrade;
            mortarProjectile.hasScatterUpgrade = hasScatterUpgrade;
            mortarProjectile.senderPosition = transform.position;
            mortarProjectile.angleBetweenSubProjectiles = angleBetweenSubProjectiles;
            mortarProjectile.subProjectilesDamageMultiplier = subProjectilesDamageMultiplier;
            mortarProjectile.subProjectilesSpeedMultiplier = subProjectilesSpeedMultiplier;
            mortarProjectile.subProjectilesRadiusMultiplier = subProjectilesRadiusMultiplier;
            mortarProjectile.flameFieldDamageMultiplier = flameFieldDamageMultiplier;
            mortarProjectile.needSound = true;
            
            mortarProjectile.hasFireChanceUpgrade = hasFireChanceUpgrade;
            mortarProjectile.chanceToSetOnFire = chanceToSetOnFire;
            mortarProjectile.burnTime = burnTime;
            mortarProjectile.burnDamageMultiplier = burnDamageMultiplier;
            
            shootDelayTimer = 1f / GetAttackSpeed();
            
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }
}