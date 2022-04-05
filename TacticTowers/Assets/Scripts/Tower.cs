using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [NonSerialized] public int upgradeCost = 10;
    
    public float shootAngle;
    public float shootDirection;
    
    public float shootDistance;
    [SerializeField] public float Dmg;
    [SerializeField] public float shootDelay;
    [NonSerialized] public bool canShoot = true;

    public ShootZone shootZone;

    public List<Upgrade> upgrades;
    
    protected void Update()
    {
        FindTarget();
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
        
        if (target != null && canShoot) Shoot(target);
    }

    protected virtual void Shoot(GameObject enemy)
    {
        
    }
}