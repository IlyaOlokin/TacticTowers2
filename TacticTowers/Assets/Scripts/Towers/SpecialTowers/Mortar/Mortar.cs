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
    [NonSerialized] public bool hasScatterUpgrade;
    [NonSerialized] public bool hasUpgrade;

    private void Start() => audioSrc = GetComponent<AudioSource>();
    private new void Update() => base.Update();

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;
        
        LootAtTarget(enemy);
        
        if (shootDelayTimer <= 0)
        {
            var newBullet =  Instantiate(bullet, transform.position, towerCanon.transform.rotation);
            MortarProjectile mortarProjectile = newBullet.GetComponent<MortarProjectile>();
            mortarProjectile.Dmg = GetDmg();
            mortarProjectile.Speed = bulletSpeed;
            mortarProjectile.radius = explosionRadius * explosionRadiusMultiplier;
            mortarProjectile.targetPos = enemy.transform.position;
            mortarProjectile.hasFlameFieldUpgrade = hasFlameFieldUpgrade;
            mortarProjectile.hasScatterUpgrade = hasScatterUpgrade;
            mortarProjectile.senderPosition = transform.position;
            
            shootDelayTimer = 1f / GetAttackSpeed();
            
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }
}