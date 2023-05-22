using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    [NonSerialized] public readonly int[] upgradePrices = {10, 25, 50, 100, 150, 225, 325, 475, 700, 1000, 1400, 2000, 2750, 3750, 5000, 6500, 8250, 10000, 12000};
    [NonSerialized] public int upgradeLevel = 1;

    [NonSerialized] public List<GameObject> enemiesToIgnore = new List<GameObject>();

    public float shootDirection;
    private Vector2 shootDirVector;

    [Header("Description")]
    public string towerName; 
    [Multiline]public string towerDescription;
    
    [Header("Visual")]
    public TowerSprites[] towerSprites;
    [SerializeField] private SpriteRenderer basementSprite;
    [SerializeField] private SpriteRenderer cannonSprite;
    [SerializeField] private SpriteRenderer additionalSprites;
    [NonSerialized] public int currentVisualSpriteIndex;
    
    [Header("Upgrades")]
    public List<CommonUpgrade> upgrades;
    public List<SpecialUpgrade> specialUpgrades;

    [Header("References")]
    [SerializeField] protected GameObject towerCanon;
    public ShootZone shootZone;

    [Header("Stats")]
    public float shootAngle;
    [NonSerialized] public float multiplierShootAngle = 1;
    
    public float shootDistance;
    [NonSerialized] public float multiplierShootDistance = 1;
    
    public float Dmg;
    [NonSerialized] public float multiplierDmg = 1;
    
    public float attackSpeed;
    [NonSerialized] public float multiplierAttackSpeed = 1;
    
    
    protected float shootDelayTimer;
    [NonSerialized] public bool isDragging = false;
    [NonSerialized] private bool isDisarmed = false;
    [NonSerialized] private bool hasParasite = false;
    private float parasiteAttackSpeedMultiplier = 1;


    
    [NonSerialized] public List<bool> upgradedSpecilaUpgrades = new List<bool> {false, false, false};

    protected AudioSource audioSrc;

    private void Awake()
    {
        shootDirVector = GetShootDirInVector(shootDirection);
    }

    private static Vector2 GetShootDirInVector(float shootDir)
    {
        return new Vector2((float) Math.Cos(shootDir / 180f * Math.PI),
            (float) Math.Sin(shootDir / 180f * Math.PI)).normalized;
    }

    protected void Update()
    {
        Shoot(FindTarget());

        if (shootDelayTimer > 0) shootDelayTimer -= Time.deltaTime;
    }

    private GameObject FindTarget()
    {
        if (!CanShoot() || EnemySpawner.enemies.Count == 0) return null;
        GameObject target = null;
        float distToTarget = float.MaxValue;
        foreach (var enemy in EnemySpawner.enemies)
        {
            if (enemy == null) continue;
            if (enemiesToIgnore.Contains(enemy) || enemy.GetComponent<Enemy>().GetInvulnerability()) 
                continue;
            var distToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            Vector3 dir = (enemy.transform.position - transform.position).normalized;
            var angle = Vector2.Angle(dir, shootDirVector);

            if (distToEnemy <= GetShootDistance())
            {
                if (target == null || distToEnemy < distToTarget)
                {
                    if (angle <= GetShootAngle() / 2f) 
                    {
                        distToTarget = distToEnemy;
                        target = enemy;
                    }
                }
            }
        }

        return target;
    }
    protected GameObject FindTarget(IEnumerable<GameObject> targetsToIgnore)
    {
        if (!CanShoot() || EnemySpawner.enemies.Count == 0) return null;
        GameObject target = null;
        float distToTarget = float.MaxValue;
        foreach (var enemy in EnemySpawner.enemies)
        {
            if (enemy == null) continue;
            if (enemiesToIgnore.Contains(enemy) 
                || targetsToIgnore.Contains(enemy) 
                || enemy.GetComponent<Enemy>().GetInvulnerability()) 
                continue;
            var distToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            Vector3 dir = (enemy.transform.position - transform.position).normalized;
            var angle = Vector2.Angle(dir, shootDirVector);
            
            if (distToEnemy <= GetShootDistance())
            {
                if (target == null || distToEnemy < distToTarget)
                {
                    if (angle <= GetShootAngle() / 2f) 
                    {
                        distToTarget = distToEnemy;
                        target = enemy;
                    }
                }
            }
        }

        return target;
    }

    protected virtual void Shoot(GameObject enemy)
    {
        
    }

    protected void LootAtTarget(Vector3 target)
    {
        Vector3 dir = transform.position - target;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        towerCanon.transform.eulerAngles = new Vector3(0, 0, angle + 90);
    }

    public float GetDmg()
    {
        return Dmg * multiplierDmg * Technologies.DmgMultiplier * GlobalBaseEffects.GetGlobalBaseDmgMultiplier(shootDirection);
    }
    
    public float GetAttackSpeed()
    {
        return attackSpeed * multiplierAttackSpeed * Technologies.AttackSpeedMultiplier * GlobalBaseEffects.GetGlobalBaseAttackSpeedMultiplier(shootDirection) * parasiteAttackSpeedMultiplier;
    }
    
    public float GetShootAngle()
    {
        return shootAngle * multiplierShootAngle * Technologies.ShootAngleMultiplier * GlobalBaseEffects.GetGlobalBaseShootAngleMultiplier(shootDirection);
    }
    
    public float GetShootDistance()
    {
        return shootDistance * multiplierShootDistance * Technologies.ShootDistanceMultiplier * GlobalBaseEffects.GetGlobalBaseShootDistanceMultiplier(shootDirection);
    }

    public void GetMultipliers(Tower tower)
    {
        multiplierDmg = tower.multiplierDmg;
        multiplierAttackSpeed = tower.multiplierAttackSpeed;
        multiplierShootAngle = tower.multiplierShootAngle;
        multiplierShootDistance = tower.multiplierShootDistance;
    }
    
    public void ReplaceTower(Tower tower)
    {
        transform.parent = tower.transform.parent;
        shootDirection = tower.shootDirection;
        shootDirVector = GetShootDirInVector(shootDirection);
        transform.rotation = tower.transform.rotation;
        shootZone = tower.shootZone;
        shootZone.tower = this;
        upgradeLevel = tower.upgradeLevel;
        enemiesToIgnore = tower.enemiesToIgnore;
    }
    
    public static Vector3? CheckWallCollision(Vector3 origin, Vector3 target, float shootDistance, bool shouldPenetrate)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, target - origin, 100f, LayerMask.GetMask("Wall"));
        if (hit.collider != null )
        {
            if (hit.distance < Vector3.Distance(origin, target))
                return hit.point;
            if (hit.distance < shootDistance && shouldPenetrate)
                return hit.point;
        }

        return null;
    }

    public static Vector3 GetRayImpactPoint(Vector3 origin, Vector3 target, bool shouldPenetrate)
    {
        var wallCollision = CheckWallCollision(origin, target, 100f, shouldPenetrate);
        if (wallCollision != null)
            return (Vector3) wallCollision;

        return target;
    }
    
    protected GameObject FindClosetEnemy(Vector3 endPos, IEnumerable<GameObject> pickedEnemies, float searchDist)
    {
        GameObject newEnemy = null;
        var minDist = float.MaxValue;
        foreach (var e in EnemySpawner.enemies)
        {
            var distance = Vector3.Distance(endPos, e.transform.position);
            if (distance <= searchDist && distance < minDist &&
                !pickedEnemies.Contains(e))
            {
                newEnemy = e;
                minDist = distance;
            }
        }

        return newEnemy;
    }
    
    protected bool IsCriticalShot(float critChance)
    {
        if (Random.Range(0f, 1f) < critChance)
            return true;
        
        return false;
    }

    public bool CanShoot()
    {
        return !isDragging && !isDisarmed;
    }

    public void Disarm(float duration)
    {
        isDisarmed = true;
        StopCoroutine("LostDisarm");
        StartCoroutine("LostDisarm", duration);
    }

    public bool IsDisarmed()
    {
        return isDisarmed;
    }

    private IEnumerator LostDisarm(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDisarmed = false;
    }

    public bool HasParasite() => hasParasite;

    public void GetParasite(float multiplier)
    {
        hasParasite = true;
        parasiteAttackSpeedMultiplier = multiplier;
    }

    public void LostParasite()
    {
        hasParasite = false;
        parasiteAttackSpeedMultiplier = 1;
    }

    public void UpgradeVisual()
    {
        basementSprite.sprite = towerSprites[currentVisualSpriteIndex].basementSprite;
        if (cannonSprite != null)
            cannonSprite.sprite = towerSprites[currentVisualSpriteIndex].cannonSprite;
        if (additionalSprites != null)
            additionalSprites.sprite = towerSprites[currentVisualSpriteIndex].additionalSprite;
    }
}