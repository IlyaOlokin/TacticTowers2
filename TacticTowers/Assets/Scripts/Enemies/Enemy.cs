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
    public EnemyType enemyType;
    
    protected Animator animator;
    protected IEnemyMover EnemyMover;
    protected Rigidbody2D rb;
    protected Healthbar hpBar;
    [SerializeField] protected bool isHpHidden;
    
    [Header("Resists")]
    [SerializeField] private bool isImmuneToFire = false;
    [SerializeField] private bool isImmuneToFreeze = false;
    [SerializeField] private float knockBackResist = 0f;
    [SerializeField] private float stunResist = 0f;
    [SerializeField] protected bool isImmortal;
    protected bool isInvulnerable;
    
    [Header("Stats")]
    [SerializeField] protected float hp;
    [SerializeField] protected float dmg;
    [SerializeField] protected int weight;
    [SerializeField] protected float initialSpeed;
    [SerializeField] protected int creditsDropChance;
    protected float cost;
    protected float rotationSpeed = 160f;
    protected bool isDead;
    
    private bool isReadyForStun = true;
    private Coroutine currentSlow;
    private bool isGround;
    
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        InitHPBar();

        var navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            EnemyMover = new EnemyMoverGround(GetComponent<NavMeshAgent>(), initialSpeed,
                GameObject.FindGameObjectWithTag("Base").transform.position);
            isGround = true;
            EnemyMover.Move(transform, Time.deltaTime);
        }
        else
            EnemyMover = new EnemyMoverAir(initialSpeed, GameObject.FindGameObjectWithTag("Base").transform.position);

        RandomizeSpeed();
    }

    public Vector3 GetPixelSize()
    {
        var worldSize = GetComponent<SpriteRenderer>().sprite.rect.size /
                        GetComponent<SpriteRenderer>().sprite.pixelsPerUnit
                        * transform.lossyScale;
        
        var screenSize = 0.5f * worldSize / Camera.main.orthographicSize;
        screenSize.y *= Camera.main.aspect;
 
        var pixelSize = 0.5f * new Vector3(screenSize.x * Camera.main.pixelWidth, screenSize.y * Camera.main.pixelHeight, 0);
 
        return pixelSize;
    }
    
    public void Start()
    {
        GetPixelSize();
    }

    public void Update()
    {
        RotateByVelocity();
    }

    public void FixedUpdate()
    {
        if (isGround && EnemyMover.IsBuildingPath())
        {
            EnemyMover.ForceMove(transform, Time.deltaTime);
            return;
        }
        EnemyMover.Move(transform, Time.deltaTime);
    }

    public int GetWeight() => weight;

    public bool GetInvulnerability() => isInvulnerable;
    
    public float GetHp() => hp;

    public void SetHp(float newHp) => hp = newHp;
    
    public void SetCost(float newCost) => cost = newCost;

    public void SetCreditsDropChance(int newDropChance) => creditsDropChance = newDropChance;
    
    public void SetHpHidden(bool isHidden) => isHpHidden = isHidden;

    public void MultiplySpeed(float multiplier)
    {
        
        EnemyMover.MultiplySpeed(multiplier);
    }

    public virtual void ExecuteAbility() { }

    public void TakeFire(FireStats newFire)
    {
        if (isImmuneToFire || isInvulnerable)
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
        if (isImmuneToFreeze && !hasSpecial || isInvulnerable)
            return;
        
        var currentFreeze = GetComponent<Freeze>() ?? gameObject.AddComponent<Freeze>();

        if (newFreeze.FreezeTime > currentFreeze.freezeTime)
        {
            if (currentFreeze.frozen && newFreeze.FreezeStacksNeeded == 1)
            {
                currentFreeze.UnfreezeInstantly();
            }
            
            currentFreeze.freezeTime = newFreeze.FreezeTime;
            currentFreeze.freezeStacksNeeded = newFreeze.FreezeStacksNeeded;
            currentFreeze.freezeStacksPerHt = newFreeze.FreezeStacksPerHit;
        }
        if (!currentFreeze.frozen) 
            currentFreeze.GetFreezeStack();
    }

    public void TakeForce(float force, Vector3 dir)
    {
        if (isInvulnerable || knockBackResist >= 1)
            return;
        
        rb.AddForce(dir.normalized * (force * (1 - knockBackResist)), ForceMode2D.Impulse);
    }

    public bool TakeDamage(float dmg, DamageType damageType, Vector3 damagerPos, bool isCritical = false)
    {
        if (isInvulnerable)
            return false;
        
        if (Freeze.GetActiveFrozenDamageMultiplier() && TryGetComponent<Freeze>(out var freeze))
            if (freeze.frozen)
                dmg *= Freeze.GetGlobalFrozenMultiplier();
            
        hp -= dmg;
        if(hpBar != null && !isHpHidden)
            hpBar.SetHealth(hp);
        
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
        if (isInvulnerable)
            return;
        
        EnemyMover.ApplySlow(slowAmount);
        if (currentSlow != null)
            StopCoroutine(currentSlow);
        currentSlow = StartCoroutine(nameof(BeSlowed), duration);
    }

    public void TakeSlow(Func<float, float> slowFunc, float duration)
    {
        if (isInvulnerable)
            return;
        
        EnemyMover.ApplySlow(slowFunc);
        if (currentSlow != null)
            StopCoroutine(currentSlow);
        currentSlow = StartCoroutine(nameof(BeSlowed), duration);
    }
    
    public void TakeStun(float duration, float stunCd)
    {
        if (!isReadyForStun && stunCd != 0 || isInvulnerable || stunResist >= 1)
            return;
        
        EnemyMover.StopMovement();
        isReadyForStun = false;
        animator.enabled = false;
        StopCoroutine(nameof(BeStunned));
        StartCoroutine(nameof(BeStunned), duration * (1 - stunResist));
        
        StartCoroutine(nameof(GetReadyForStun), stunCd);
    }
    
    protected void RotateByVelocity()
    {
        if (EnemyMover.IsStopped() || EnemyMover.IsBuildingPath()) 
            return;
        
        var angle = EnemyMover.GetRotationAngle(transform.position);
        var targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    
    protected void RandomizeSpeed()
    {
        if (GetComponent<Boss>() != null) 
            return;
        
        EnemyMover.RandomizeSpeed();
    }

    private IEnumerator BeSlowed(float duration)
    {
        yield return new WaitForSeconds(duration);
        EnemyMover.ResetSpeed();
    }
    
    private IEnumerator BeStunned(float duration)
    {
        yield return new WaitForSeconds(duration);
        animator.enabled = true;
        EnemyMover.StartMovement();
        
        if (TryGetComponent<Freeze>(out var freeze))
        {
            freeze.UnfreezeInstantly();
            Destroy(freeze);
        }
    }

    private IEnumerator GetReadyForStun(float stunCd)
    {
        yield return new WaitForSeconds(stunCd);
        isReadyForStun = true;
    }

    public void SetMultipliers(float hpMultiplier, float speedMultiplier, float creditsDropChanceMultiplier)
    {
        hp *= hpMultiplier;
        InitHPBar();
        MultiplySpeed(speedMultiplier);
        creditsDropChance = (int) (creditsDropChance * creditsDropChanceMultiplier);
    }

    private void DropCreditsByChance(int chance)
    {
        if (Random.Range(0, 100) < chance) 
            Credits.AddSessionCredits(weight);
    }
    
    private void InitHPBar()
    {
        hpBar = GetComponentInChildren<Healthbar>();
        if (hpBar != null)
            hpBar.SetHealth(hp);
    }
    
    private void OnDeath(DamageType damageType, Vector3 killerPos)
    {
        Money.AddMoney(cost);
        
        isDead = true;
        EnemyMover.StopMovement();
        
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            if (dmg == 0) return;
            other.gameObject.GetComponent<Base>().TakeDamage(dmg);
            OnDeath(DamageType.Normal, other.transform.position);
        }
    }
    
    private void OnDestroy()
    {
        EnemySpawner.enemies.Remove(gameObject);
    }
}
