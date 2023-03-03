using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [NonSerialized] public readonly int[] upgradePrices = {10, 25, 50, 100, 150, 225, 325, 475, 700, 1000, 1400, 2000, 2750, 3750, 5000, 6500, 8250, 10000, 12000};
    [NonSerialized] public int upgradeLevel = 1;

    [NonSerialized] public List<GameObject> enemiesToIgnore = new List<GameObject>();

    public float shootDirection;

    [Header("Description")]
    public string towerName; 
    [Multiline]public string towerDescription;
    
    [Header("Visual")]
    public Sprite[] towerSprites;
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
            if (enemiesToIgnore.Contains(enemy)) continue;
            var distToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            Vector3 dir = transform.position - enemy.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
            if (distToEnemy <= GetShootDistance())
            {
                if (target == null || distToEnemy < distToTarget)
                {
                    if (Math.Abs(shootDirection - angle) <= GetShootAngle() / 2f
                    || shootDirection == 0 && Math.Abs(360 - angle) <= GetShootAngle() / 2f) // костыль
                    {
                        distToTarget = distToEnemy;
                        target = enemy;
                    }
                }
            }
        }

        return target;
    }
    protected GameObject FindTarget(List<GameObject> targetsToIgnore)
    {
        if (!CanShoot() || EnemySpawner.enemies.Count == 0) return null;
        GameObject target = null;
        float distToTarget = float.MaxValue;
        foreach (var enemy in EnemySpawner.enemies)
        {
            if (enemy == null) continue;
            if (enemiesToIgnore.Contains(enemy) || targetsToIgnore.Contains(enemy)) continue;
            var distToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            Vector3 dir = transform.position - enemy.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
            if (distToEnemy <= GetShootDistance())
            {
                if (target == null || distToEnemy < distToTarget)
                {
                    if (Math.Abs(shootDirection - angle) <= GetShootAngle() / 2f
                        || shootDirection == 0 && Math.Abs(360 - angle) <= GetShootAngle() / 2f) // костыль
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

    protected void LootAtTarget(GameObject target)
    {
        Vector3 dir = transform.position - target.transform.position;
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
}