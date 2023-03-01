using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [Header("Damage Over Distance Upgrade")]
    [SerializeField] private float damageOverDistanceMultiplier = 0.05f;
    
    [NonSerialized] public bool hasDoubleShotUpgrade;
    [Header("Double Shot Upgrade")]
    [SerializeField] private float doubleShotChance;
    [SerializeField] private float doubleShotDelay;

    private void Start() => audioSrc = GetComponent<AudioSource>();
    private new void Update() => base.Update();

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;
        
        LootAtTarget(enemy);
        
        if (shootDelayTimer <= 0)
        {
            int shotsCount = 1;
            if (hasDoubleShotUpgrade && Random.Range(0f, 1f) < doubleShotChance) shotsCount = 2;
            
            StartCoroutine(MakeMultipleShots(shotsCount, doubleShotDelay));

            shootDelayTimer = 1f / GetAttackSpeed();
        }
    }

    private void MakeShot()
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
                    distanceMultiplier = 1f + Vector2.Distance(transform.position, hit.transform.position) *
                        damageOverDistanceMultiplier;
                }

                if (multiplier < minDmg * minDmgMultiplier) multiplier = minDmg * minDmgMultiplier;
                newEnemy.TakeDamage(GetDmg() * multiplier * distanceMultiplier, damageType, transform.position);
                multiplier *= dmgMultiplier * dmgMultiplierMultiplier;
            }
        }
        audioSrc.PlayOneShot(audioSrc.clip);
    }
    
    private IEnumerator MakeMultipleShots(int shotsCount, float delayInSeconds)
    {
        for (int i = 0; i < shotsCount; i++)
        {
            MakeShot();
            yield return new WaitForSeconds(delayInSeconds);
        }
    }
}