using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    protected IPathFinder pathFinder;
    private Rigidbody2D rb;
    
    [Header("Stats")]
    [FormerlySerializedAs("IsImmuneToFire")] [SerializeField] private bool isImmuneToFire = false;
    [FormerlySerializedAs("IsImmuneToFreeze")] [SerializeField] private bool isImmuneToFreeze = false;
    [FormerlySerializedAs("KnockBackResist")] [SerializeField] private float knockBackResist = 0f;
    [SerializeField] private float stunResist;
    
    [SerializeField] protected bool isImmortal;
    [SerializeField] protected float hp;
    [SerializeField] protected float dmg;
    [SerializeField] protected int weight;
    [SerializeField] protected float stunCd = 5f;
    protected float cost;
    protected bool isDead;
    protected float rotationSpeed = 160f;
    
    [NonSerialized] public bool hasTentacle; // TODO: убрать
    [SerializeField] private int creditsDropChance;

    [SerializeField] private bool isReadyForStun = true;
    
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

    public void MultiplySpeed(float multiplier) => pathFinder.MultiplySpeed(multiplier);

    public virtual void ExecuteAbility() { }

    public void TakeFire(FireStats newFire)
    {
        if (isImmuneToFire)
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
        if (isImmuneToFreeze && !hasSpecial)
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
        rb.AddForce(dir.normalized * (force * (1 - knockBackResist)), ForceMode2D.Impulse);
    }

    public bool TakeDamage(float dmg, DamageType damageType, Vector3 damagerPos, bool isCritical = false)
    {
        hp -= dmg;
        var newEffect = Instantiate(EnemyVFXManager.Instance.GetEffect("DamageNumber").effect, transform.position, Quaternion.identity);
        newEffect.GetComponent<DamageNumberEffect>().WriteDamage(dmg);
        newEffect.GetComponent<DamageNumberEffect>().InitTargetPos(damagerPos, isCritical);
        if (hp < 0 && isDead) 
            return true;
        
        if (hp <= 0 && !isImmortal)
        {
            OnDeath(damageType, damagerPos);
            return true;
        }
        
        return false;
    }
    
    public void TakeSlow(float slowAmount, float duration)
    {
        pathFinder.SlowMovement(slowAmount);
        StartCoroutine(nameof(BeSlowed), duration);
    }

    public void TakeSlow(Func<float, float> slowFunc, float duration)
    {
        pathFinder.ApplySlow(slowFunc);
        StartCoroutine(nameof(BeSlowed), duration);
    }
    
    public void TakeStun(float duration, bool isStartingCd)
    {
        if (!isReadyForStun)
            return;
        
        pathFinder.StopMovement();
        isReadyForStun = false;
        StartCoroutine(nameof(BeStunned), duration * (1 - stunResist));
        
        if (isStartingCd)
            StartCoroutine(nameof(GetReadyForStun));
    }

    public IEnumerator BeSlowed(float duration)
    {
        yield return new WaitForSeconds(duration);
        pathFinder.ResetSpeed();
    }
    
    private IEnumerator BeStunned(float duration)
    {
        yield return new WaitForSeconds(duration);
        pathFinder.StartMovement();
    }
    
    private IEnumerator GetReadyForStun()
    {
        yield return new WaitForSeconds(stunCd);
        isReadyForStun = true;
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
        if (Random.Range(0, 100) < chance) 
            Credits.AddSessionCredits(weight);
    }
    
    public void SetTentacle()
    {
        hasTentacle = true;
    }
}
