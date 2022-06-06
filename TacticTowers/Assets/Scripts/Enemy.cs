using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float dmg;
    [SerializeField] private float hp;
    
    [NonSerialized] public float cost;
    [SerializeField] private int creditsDropChance;
    public int weight;
    [SerializeField] private GameObject damageNumberEffect;
    [SerializeField] private GameObject deathParticles;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(GameObject.FindGameObjectWithTag("Base").transform.position);
    }

    
    void Update()
    {
        RotateByVelocity();
    }

    private void RotateByVelocity()
    {
        var angle = Mathf.Atan2(agent.velocity.y, agent.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            other.gameObject.GetComponent<Base>().TakeDamage(dmg);
            OnDeath();
        }
    
    }
    
    public void TakeDamage(float dmg)
    {
        if (hp < 0) return;
        hp -= dmg;
        var newEffect = Instantiate(damageNumberEffect, transform.position, Quaternion.identity);
        newEffect.GetComponent<DamageNumberEffect>().WriteDamage(dmg * 5);
        if (hp <= 0)
        {
            OnDeath();
        }
    }
    private void OnDeath()
    {
        EnemySpawner.enemies.Remove(gameObject);
        Money.AddMoney(cost * Technologies.MoneyMultiplier);
        DropCreditsByChance(creditsDropChance);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void DropCreditsByChance(int chance)
    {
        if (Random.Range(0, 100) < chance) Credits.AddSessionCredits(weight);
    }
}
