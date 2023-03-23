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
    
    [SerializeField] protected bool isImmortal;
    [SerializeField] protected float hp;
    [SerializeField] protected float dmg;
    [SerializeField] protected int weight;
    protected float cost;
    protected bool isDead;
    protected float rotationSpeed = 160f;

    [NonSerialized] public bool hasTentacle; // TODO: убрать
    [SerializeField] private int creditsDropChance;
    
    public void Start()
    {
        pathFinder = new PathFinderGround(GetComponent<NavMeshAgent>());
        rb = GetComponent<Rigidbody2D>();
 
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
        rb.AddForce(dir.normalized * (force * (1 - KnockBackResist)), ForceMode2D.Impulse);
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
        
        DropCreditsByChance(creditsDropChance);
        switch (damageType)
        {
            case DamageType.Normal:
                new DeathNormal().PlayEffect(gameObject, killerPos);
                break;
                
            case DamageType.Fire:
                new DeathFire().PlayEffect(gameObject, killerPos);
                break;
        }
        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner.enemies.Remove(gameObject);
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
