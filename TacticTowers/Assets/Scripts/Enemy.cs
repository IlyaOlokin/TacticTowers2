using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float dmg;
    [SerializeField] private float hp;
    
    [NonSerialized] public float cost;
    public int weight;
    [SerializeField] private GameObject damageNumberEffect;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(GameObject.FindGameObjectWithTag("Base").transform.position);
    }

    
    void Update()
    {
        
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
        hp -= dmg;
        var newEffect = Instantiate(damageNumberEffect, transform.position, Quaternion.identity);
        newEffect.GetComponent<DamageNumberEffect>().WriteDamage(dmg);
        if (hp <= 0)
        {
            OnDeath();
        }
    }
    private void OnDeath()
    {
        EnemySpawner.enemies.Remove(gameObject);
        Money.AddMoney(cost * GlobalMultipliers.moneyMultiplier);
        Destroy(gameObject);
    }
}
