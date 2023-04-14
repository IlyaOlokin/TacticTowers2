using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MortarProjectile : MonoBehaviour
{
    [NonSerialized] public float dmg;
    [NonSerialized] public float speed;
    [NonSerialized] public float radius;
    [NonSerialized] public Vector3 targetPos;
    [NonSerialized] public Vector3 senderPosition;
    
    private Rigidbody2D rb;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject flameField;
    private DamageType damageType = DamageType.Normal;

    [NonSerialized] public bool hasFlameFieldUpgrade;

    [NonSerialized] public float flameFieldDamageMultiplier;
    
    [NonSerialized] public bool hasScatterUpgrade;
    
    [NonSerialized] public int angleBetweenSubProjectiles;
    [NonSerialized] public float subProjectilesDamageMultiplier;
    [NonSerialized] public float subProjectilesSpeedMultiplier;
    [NonSerialized] public float subProjectilesRadiusMultiplier;
    
    [NonSerialized] public bool hasFireChanceUpgrade;
    [NonSerialized] public float chanceToSetOnFire;
    [NonSerialized] public float burnTime;
    [NonSerialized] public float burnDamageMultiplier;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) < 0.001f)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        var newExplosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        newExplosion.transform.localScale = new Vector3(radius, radius, radius) * 0.9f;
        if (hasFlameFieldUpgrade) CreateFlameField();
        if (hasScatterUpgrade) CreateScatterProjectilesField();
        DealDamage();
    }
    
    private void DealDamage()
    {
        var enemiesInRadius = new List<Enemy>();
        var allEnemies = EnemySpawner.enemies;
        foreach (var enemy in allEnemies)
        {
            if (enemy == null) continue;
            if (Vector3.Distance(transform.position, enemy.transform.position) < radius)
                enemiesInRadius.Add(enemy.GetComponent<Enemy>());
        }

        for (int i = 0; i < enemiesInRadius.Count; i++)
        { 
            if (enemiesInRadius[i] is null) continue;
            enemiesInRadius[i].TakeDamage(dmg, damageType, transform.position);
            if (hasFireChanceUpgrade && Random.Range(0f, 1f) < chanceToSetOnFire)
                enemiesInRadius[i].TakeFire(new FireStats(burnTime, dmg * burnDamageMultiplier));
        }
    }

    private void CreateFlameField()
    {
        var newFlameField = Instantiate(flameField, transform.position, quaternion.identity);
        newFlameField.GetComponent<DamageZoneBox>().damage = dmg * flameFieldDamageMultiplier;
        newFlameField.GetComponent<UpScalerOnStart>().targetScale = new Vector3(radius * 2, radius * 2, radius);
    }
    
    private void CreateScatterProjectilesField()
    {
        int subProjCount = 3;
        for (int i = 1; i <= subProjCount; i++)
        {
            var newBullet = Instantiate(gameObject, transform.position, Quaternion.identity);
            MortarProjectile mortarProjectile = newBullet.GetComponent<MortarProjectile>();
            mortarProjectile.dmg = dmg * subProjectilesDamageMultiplier;
            mortarProjectile.speed = speed * subProjectilesSpeedMultiplier;
            mortarProjectile.radius = radius * subProjectilesRadiusMultiplier;
            mortarProjectile.targetPos = GetSubProjTargetPos(subProjCount, i);
            mortarProjectile.hasFlameFieldUpgrade = hasFlameFieldUpgrade;
            mortarProjectile.hasScatterUpgrade = false;
            mortarProjectile.senderPosition = transform.position;
            
            mortarProjectile.hasFireChanceUpgrade = hasFireChanceUpgrade;
            mortarProjectile.chanceToSetOnFire = chanceToSetOnFire;
            mortarProjectile.burnTime = burnTime;
            mortarProjectile.burnDamageMultiplier = burnDamageMultiplier;
        }
    }

    private Vector3 GetSubProjTargetPos(int count, int i)
    {
        Vector3 result = new Vector3();
        if (count % 2 == 1)
            result = Quaternion.Euler(0, 0, i / 2 * angleBetweenSubProjectiles * (i % 2 == 0 ? 1 : -1f)) *
                     (transform.position - senderPosition).normalized * radius * 1.5f;
        else
            result = Quaternion.Euler(0, 0,
                         i / 2 * angleBetweenSubProjectiles * (i % 2 == 0 ? 1 : -1f) - angleBetweenSubProjectiles / 2f) *
                     (transform.position - senderPosition).normalized * radius * 1.5f;

        return transform.position + result;
    }
}
