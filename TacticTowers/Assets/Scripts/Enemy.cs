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
    [Header("Stats")]
    [SerializeField] private float dmg;
    [SerializeField] public float hp;
    
    [NonSerialized] public float cost;
    [SerializeField] private int creditsDropChance;
    public int weight;
    
    [NonSerialized] public bool hasTentacle;
    private float rotationSpeed = 160f;
    private bool isDead;
    [SerializeField] private bool isImmortal;
    
    [Header("Visual Effects")]
    [SerializeField] private GameObject damageNumberEffect;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private Material burnMaterial;
    private static readonly int fade = Shader.PropertyToID("_Fade");
    private float fadeDuration = 1.2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!agent.enabled) return;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(GameObject.FindGameObjectWithTag("Base").transform.position);
        RandomizeSpeed();
    }

    
    void Update()
    {
        RotateByVelocity();
    }

    private void RotateByVelocity()
    {
        if (!agent.enabled || agent.speed == 0) return;
        var angle = Mathf.Atan2(agent.desiredVelocity.y, agent.desiredVelocity.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            if (dmg == 0) return;
            other.gameObject.GetComponent<Base>().TakeDamage(dmg);
            OnDeath(DamageType.Normal, other.transform.position);
        }
    
    }
    
    private void RandomizeSpeed()
    {
        if (GetComponent<Boss>() != null) return;
        var multiplier = Random.Range(1f, 1.75f);
        agent.speed *= multiplier;
        agent.avoidancePriority = (int) (agent.avoidancePriority * multiplier);
    }
    
    public void TakeDamage(float dmg, DamageType damageType, Vector3 damagerPos)
    {
        
        hp -= dmg;
        var newEffect = Instantiate(damageNumberEffect, transform.position, Quaternion.identity);
        newEffect.GetComponent<DamageNumberEffect>().WriteDamage(dmg);
        if (hp < 0 && isDead) return;
        
        if (hp <= 0 && !isImmortal)
        {
            OnDeath(damageType, damagerPos);
        }
    }
    private void OnDeath(DamageType damageType, Vector3 killerPos)
    {
        Money.AddMoney(cost);
        isDead = true;
        DropCreditsByChance(creditsDropChance);
        switch (damageType)
        {
            case DamageType.Normal:
                DieNormal(killerPos);
                break;
            case DamageType.Fire:
                DieFire(burnMaterial);
                break;
        }
    }

    private void OnDestroy()
    {
        EnemySpawner.enemies.Remove(gameObject);
    }

    private void DieNormal(Vector3 killerPos)
    {
        var dir = transform.position - killerPos;
        var asin = Mathf.Asin(dir.normalized.y);
        var degrees = asin * 180 / Mathf.PI;
        if (dir.x < 0) degrees = 180 - degrees;
        
        Quaternion rotation = Quaternion.Euler(0,0, degrees);
        Instantiate(deathParticles, transform.position, rotation);
        Destroy(gameObject);
    }

    private void DieFire(Material newMaterial)
    {
        GetComponent<SpriteRenderer>().material = newMaterial;
        agent.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        foreach (Collider2D collider in transform.GetComponentsInChildren(typeof(Collider2D)))
        {
            collider.enabled = false;
        }
        StartCoroutine("Burn", GetComponent<SpriteRenderer>().material);
    }

    private IEnumerator Burn(Material material)
    {
        for (float alpha = fadeDuration; alpha >= 0; alpha -= Time.deltaTime)
        {
            material.SetFloat(fade, alpha / fadeDuration);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void DropCreditsByChance(int chance)
    {
        if (Random.Range(0, 100) < chance) Credits.AddSessionCredits(weight);
    }

    public void SetTentacle()
    {
        hasTentacle = true;
    }
}
