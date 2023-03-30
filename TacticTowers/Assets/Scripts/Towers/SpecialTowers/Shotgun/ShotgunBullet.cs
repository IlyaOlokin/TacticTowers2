using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet
{
    [NonSerialized] public bool hasKnockBackUpgrade;
    [NonSerialized] public float knockBackForce;

    [SerializeField] private GameObject visualEffect;

    protected override void OnEnemyHit(Collider2D other)
    {
        if (hasKnockBackUpgrade)
            other.GetComponent<Enemy>().TakeForce(knockBackForce, other.transform.position - departurePos);
        
        base.OnEnemyHit(other);
    }

    public void ActivateVisualEffect()
    {
        visualEffect.SetActive(true);
    }
}
