using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [NonSerialized] public readonly int[] upgradePrices = {10, 15, 20, 25, 30, 40, 50, 60, 70, 85, 100, 115, 130, 150};
    [NonSerialized] public int upgradeLevel = 1;
    
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
    [NonSerialized] public bool canShoot = true;

    private int disarmStacks = 0;
    private int disarmStacksNeed = 3;

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
        
        if (canShoot) Shoot(target);
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
        return attackSpeed * multiplierAttackSpeed * GlobalBaseEffects.GetGlobalBaseAttackSpeedMultiplier(shootDirection);
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
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Web")
        {
            disarmStacks++;
            if(disarmStacks >= disarmStacksNeed)
            {
                StartCoroutine(Disarm());
                disarmStacks = 0;
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator Disarm()
    {
        canShoot = false;
        yield return new WaitForSeconds(3.5f);
        canShoot = true;
    }
}