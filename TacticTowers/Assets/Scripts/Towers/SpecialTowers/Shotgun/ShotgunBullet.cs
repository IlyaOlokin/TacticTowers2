using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet
{
    [NonSerialized] public bool hasKnockBackUpgrade;
    [NonSerialized] public float knockBackForce;
    
    protected override void OnEnemyHit(Collider2D other)
    {
        if (hasKnockBackUpgrade)
            other.GetComponent<Enemy>().TakeForce(knockBackForce, other.transform.position - departurePos);
        
        base.OnEnemyHit(other);
    }
}
