using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [NonSerialized] public readonly int[] upgradePrices = {10, 25, 40, 100, 150, 250, 500, 800, 1400, 2000, 3000, 4200, 5500, 7500};
    [NonSerialized] public int upgradeLevel = 1;

    [NonSerialized] public List<GameObject> enemiesToIgnore = new List<GameObject>();

    public float shootDirection;

    public string towerName; 
    [Multiline]public string towerDescription;
    public Sprite towerSprite;

    [SerializeField] protected GameObject towerCanon;

    [SerializeField] private  float shootAngle;
    public float multiplierShootAngle = 1;
    
    [SerializeField] private  float shootDistance;
    public float multiplierShootDistance = 1;
    
    [SerializeField] private float Dmg;
    public float multiplierDmg = 1;
    
    [SerializeField] private float attackSpeed;
    public float multiplierAttackSpeed = 1;
    
    
    protected float shootDelayTimer;
    [NonSerialized] public bool isDragging = false;
    [NonSerialized] private bool isDisarmed = false;
    [NonSerialized] private bool hasParasite = false;
    private float parasiteAttackSpeedMultiplier = 1;

    public ShootZone shootZone;

    public List<Upgrade> upgrades;
    
    protected void Update()
    {
        FindTarget();
        
        if (shootDelayTimer > 0) shootDelayTimer -= Time.deltaTime;
    }

    private void FindTarget()
    {
        GameObject target = null;
        float distToTarget = float.MaxValue;
        if (EnemySpawner.enemies.Count == 0)
        {
            Shoot(null);
            return;
        }
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
        
        if (CanShoot()) Shoot(target);
        else Shoot(null);
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

    protected float GetDmg()
    {
        return Dmg * multiplierDmg * Technologies.DmgMultiplier * GlobalBaseEffects.GetGlobalBaseDmgMultiplier(shootDirection);
    }
    
    protected float GetAttackSpeed()
    {
        return attackSpeed * multiplierAttackSpeed * GlobalBaseEffects.GetGlobalBaseAttackSpeedMultiplier(shootDirection) * parasiteAttackSpeedMultiplier;
    }
    
    public float GetShootAngle()
    {
        return shootAngle * multiplierShootAngle * Technologies.ShootAngleMultiplier * GlobalBaseEffects.GetGlobalBaseShootAngleMultiplier(shootDirection);
    }
    
    public float GetShootDistance()
    {
        return shootDistance * multiplierShootDistance * GlobalBaseEffects.GetGlobalBaseShootDistanceMultiplier(shootDirection);
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
    
    public static Vector3? CheckWallCollision(Vector3 origin, Vector3 target, bool shouldPenetrate)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, target - origin, 100f, LayerMask.GetMask("Wall"));
        if (hit.collider != null && (hit.distance < Vector3.Distance(origin, target) || shouldPenetrate))
            return hit.point;
        
        return null;
    }

    public static Vector3 GetRayImpactPoint(Vector3 origin, Vector3 target, bool shouldPenetrate)
    {
        var wallCollision = CheckWallCollision(origin, target, shouldPenetrate);
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