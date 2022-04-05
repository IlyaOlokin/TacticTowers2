using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     [NonSerialized] public float Dmg;
     [NonSerialized] public float Speed;
    
     private Rigidbody2D rb;
    
    [SerializeField] public Vector2 velocityDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * Speed;
    }

    public void OnCreate()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(Dmg);
        }
        Destroy(gameObject);
    }
    
}