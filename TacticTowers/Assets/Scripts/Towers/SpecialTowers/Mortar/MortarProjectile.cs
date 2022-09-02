using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarProjectile : MonoBehaviour
{
    [NonSerialized] public float Dmg;
    [NonSerialized] public float Speed;
    [NonSerialized] public float radius;
    [NonSerialized] public Vector3 targetPos;
    
    private Rigidbody2D rb;
    [SerializeField] private GameObject explosionEffect;
    private DamageType damageType = DamageType.Fire;

    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
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
        DealDamage();
    }
    
    private void DealDamage()
    {
        var enemiesInRadius = new List<Enemy>();
        foreach (var enemy in EnemySpawner.enemies)
        {
            
            if (Vector3.Distance(transform.position, enemy.transform.position) < radius)
                enemiesInRadius.Add(enemy.GetComponent<Enemy>());
        }

        for (int i = 0; i < enemiesInRadius.Count; i++)
        {
            enemiesInRadius[i].TakeDamage(Dmg, damageType);
        }
            
    }
}
