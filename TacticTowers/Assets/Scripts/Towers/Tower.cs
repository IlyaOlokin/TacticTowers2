using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [NonSerialized] public int upgradeCost = 5;
    [NonSerialized] public int upgradeIncrement = 5;
    [NonSerialized] public int upgradeLevel = 1;

    public float shootAngle;
    public float shootDirection;
    
    public float shootDistance;
    [SerializeField] public float Dmg;
    [SerializeField] public float attackSpeed;
    protected float shootDelayTimer;
    [NonSerialized] public bool canShoot = true;

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
        if (EnemySpawner.enemies.Count == 0) return;
        foreach (var enemy in EnemySpawner.enemies)
        {
            if (enemy == null) continue;
            var distToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            Vector3 dir = transform.position - enemy.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
            if (distToEnemy <= shootDistance)
            {
                if (target == null || distToEnemy < distToTarget)
                {
                    if (Math.Abs(shootDirection - angle) <= shootAngle / 2f
                    || shootDirection == 0 && Math.Abs(360 - angle) <= shootAngle / 2f) // костыль
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
        transform.eulerAngles = new Vector3(0, 0, angle + 90);
    }
}