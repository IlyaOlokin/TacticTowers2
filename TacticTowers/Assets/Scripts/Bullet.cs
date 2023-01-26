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
     private DamageType damageType = DamageType.Normal;
    
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
        
        Physics2D.IgnoreLayerCollision(6, 12);
    }
     private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(Dmg, damageType, departurePos);
        }
        Destroy(gameObject);
    }
    
}