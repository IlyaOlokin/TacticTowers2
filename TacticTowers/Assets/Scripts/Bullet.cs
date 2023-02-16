using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     [NonSerialized] public float Dmg;
     [NonSerialized] public float Speed;
     [NonSerialized] public List<GameObject> enemiesToIgnore;
     [NonSerialized] public Vector3 departurePos;
     [NonSerialized] public bool hasPenetrationUpgrade;
     [NonSerialized] public float penetrationDamageMultiplier;
     [NonSerialized] public int penetrationsCount;
     private DamageType damageType = DamageType.Normal;
     private int penetrationsLeft = 0;
    
     private Rigidbody2D rb;

     private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * Speed;
        foreach (var enemy in enemiesToIgnore)
        {
            if (enemy is null) continue;
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (hasPenetrationUpgrade)
            penetrationsLeft = penetrationsCount;
        Physics2D.IgnoreLayerCollision(6, 12);
    }
     
     private void OnTriggerEnter2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("Enemy"))
         {
             other.gameObject.GetComponent<Enemy>().TakeDamage(Dmg, damageType, departurePos);
             if (penetrationsLeft == 0)
             {
                 Destroy(gameObject);
                 return;
             }

             GetPenetrationEffect();
         }
         else
         {
             Destroy(gameObject);
         }
     }

     private void GetPenetrationEffect()
     {
         penetrationsLeft--;
         Dmg *= penetrationDamageMultiplier;
     }
}