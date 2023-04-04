using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBullet : Bullet
{
    [NonSerialized] public Minigun sender;
    [NonSerialized] public GameObject target;
    private bool hitEnemy;
    
    protected override void OnEnemyHit(Collider2D other)
    {
        if (!sender.hasDamageStackUpgrade)
        {
            base.OnEnemyHit(other);
            return;
        }
        
        if (!hitEnemy)
        {
            if (other.gameObject == target)
            {
                Dmg *= sender.GetBonusStackDamageMultiplier();
                sender.IncrementStacksCount();
            }
            else
            {
                sender.ResetDamageStacksCount();
            }
        }
        hitEnemy = true;

        base.OnEnemyHit(other);
    }
}
