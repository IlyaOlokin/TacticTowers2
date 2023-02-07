using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : Tower
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform railStartPos;
    [SerializeField] private LayerMask layerMask;
    public float dmgMultiplier;
    public float dmgMultiplierMultiplier;
    public float minDmg;
    public float minDmgMultiplier;
    private DamageType damageType = DamageType.Normal;

    [NonSerialized] public bool hasDamageOverDistanceUpgrade;

    private void Start() => audioSrc = GetComponent<AudioSource>();
    private new void Update() => base.Update();

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;
        
        LootAtTarget(enemy);
        
        if (shootDelayTimer <= 0)
        {
            
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, towerCanon.transform.up, Mathf.Infinity, layerMask);
            var newRail = Instantiate(bullet, transform.position, towerCanon.transform.rotation);
            newRail.GetComponent<LineRenderer>().SetPosition(0, railStartPos.position);
            newRail.GetComponent<LineRenderer>().SetPosition(1, transform.position + towerCanon.transform.up * 50);
            float multiplier = 1f;
            float distanceMultiplier = 1f;
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                Enemy newEnemy = hit.transform.GetComponent<Enemy>();

                if (newEnemy)
                {
                    if (enemiesToIgnore.Contains(newEnemy.gameObject)) continue;

                    if (hasDamageOverDistanceUpgrade && i == 0)
                    {
                        distanceMultiplier = 1f + Vector2.Distance(transform.position, hit.transform.position) * 0.05f;
                    }
                    if (multiplier < minDmg * minDmgMultiplier) multiplier = minDmg * minDmgMultiplier;
                    newEnemy.TakeDamage(GetDmg() * multiplier * distanceMultiplier, damageType, transform.position);
                    multiplier *= dmgMultiplier * dmgMultiplierMultiplier;
                }
            }

            shootDelayTimer = 1f / GetAttackSpeed();
            
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }
}