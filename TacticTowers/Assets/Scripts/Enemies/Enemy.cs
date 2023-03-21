using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    protected PathFinder pathFinder;
    private Rigidbody2D rb;
    
    [Header("Stats")]
    [SerializeField] private bool IsImmuneToFire = false;
    [SerializeField] private bool IsImmuneToFreeze = false;
    [SerializeField] private float KnockBackResist = 0f;
    
    [SerializeField] protected float hp;
    [SerializeField] protected float dmg;
    [SerializeField] protected int weight;
    protected float cost;
    protected bool isDead;
    protected bool isImmortal;
    protected float rotationSpeed = 160f;

    [NonSerialized] public bool hasTentacle; // TODO: убрать
    
    /*
    private NavMeshAgent agent;
    [SerializeField] private int creditsDropChance;
    
    [Header("Visual Effects")]
    [SerializeField] private GameObject damageNumberEffect;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private Material burnMaterial;
    private static readonly int fade = Shader.PropertyToID("_Fade");
    private float fadeDuration = 1.2f;
*/
    public void Start()
    {
        pathFinder = new PathFinderGround(GetComponent<NavMeshAgent>());
        rb = GetComponent<Rigidbody2D>();
        /*
        agent = GetComponent<NavMeshAgent>();
        if (!agent.enabled || !agent.isOnNavMesh) return;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(GameObject.FindGameObjectWithTag("Base").transform.position);
        */
        RandomizeSpeed();
    }
    
    public void Update()
    {
        RotateByVelocity();
    }

    public int GetWeight() => weight;
    public float GetHp() => hp;

    public void SetHp(float newHp) => hp = newHp;
    
    public void SetCost(float newCost) => cost = newCost;

    public void TakeFire(FireStats newFire)
    {
        if (IsImmuneToFire)
            return;
        
        var currentFire = GetComponent<Fire>() ?? gameObject.AddComponent<Fire>();

        var existingFireValue = currentFire.burnTime * currentFire.burnDmg;
        var newFireValue = newFire.BurnTime * newFire.BurnDmg;

        if (newFireValue <= existingFireValue)
            return;
        
        currentFire.burnTime = newFire.BurnTime;
        currentFire.burnDmg = newFire.BurnDmg;
    }

    public void TakeFreeze(FreezeStats newFreeze, bool hasSpecial)
    {
        if (IsImmuneToFreeze && !hasSpecial)
            return;
        
        var currentFreeze = GetComponent<Freeze>() ?? gameObject.AddComponent<Freeze>();

        if (newFreeze.FreezeTime > currentFreeze.freezeTime)
        {
            if (currentFreeze.frozen && newFreeze.FreezeStacksNeeded == 1)
            {
                currentFreeze.UnfreezeInstantly();
            }
            else
            {
                currentFreeze.freezeTime = newFreeze.FreezeTime;
                currentFreeze.freezeStacksNeeded = newFreeze.FreezeStacksNeeded;
                currentFreeze.freezeStacksPerHt = newFreeze.FreezeStacksPerHit;
            }
        }
        if (!currentFreeze.frozen) 
            currentFreeze.GetFreezeStack();
    }

    public void TakeForce(float force, Vector3 dir)
    {
        rb.AddForce(dir.normalized * force * (1 - KnockBackResist), ForceMode2D.Impulse);
    }

    private void RotateByVelocity()
    {
        if (pathFinder.IsStopped()) 
            return;
        
        var angle = pathFinder.GetRotationAngle();
        var targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        
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
        if (GetComponent<Boss>() != null) 
            return;
        pathFinder.RandomizeSpeed();
    }
    
    public bool TakeDamage(float dmg, DamageType damageType, Vector3 damagerPos)
    {
        hp -= dmg;
        var newEffect = Instantiate(EnemyVFXManager.Instance.GetEffect("DamageNumber").effect, transform.position, Quaternion.identity);
        newEffect.GetComponent<DamageNumberEffect>().WriteDamage(dmg);
        if (hp < 0 && isDead) return true;
        
        if (hp <= 0 && !isImmortal)
        {
            OnDeath(damageType, damagerPos);
            return true;
        }
        return false;
    }
    
    private void OnDeath(DamageType damageType, Vector3 killerPos)
    {
        Money.AddMoney(cost);
        
        isDead = true;
        pathFinder.StopMovement();
        //agent.enabled = false;
        
        //DropCreditsByChance(creditsDropChance);
        switch (damageType)
        {
            case DamageType.Normal:
                new DeathNormal().PlayEffect(gameObject, killerPos);
                //DieNormal(killerPos);
                break;
            case DamageType.Fire:
                new DeathFire().PlayEffect(gameObject, killerPos);
                //DieFire(burnMaterial);
                break;
        }
        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner.enemies.Remove(gameObject);
    }
/*
    private void DieNormal(Vector3 killerPos)
    {
        var dir = transform.position - killerPos;
        var asin = Mathf.Asin(dir.normalized.y);
        var degrees = asin * 180 / Mathf.PI;
        if (dir.x < 0) degrees = 180 - degrees;
        
        Quaternion rotation = Quaternion.Euler(0,0, degrees);
        /Instantiate(deathParticles, transform.position, rotation);
        Destroy(gameObject);
    }

    private void DieFire(Material newMaterial)
    {
        EnemySpawner.enemies.Remove(gameObject);
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
*/
    public void SetTentacle()
    {
        hasTentacle = true;
    }
}
