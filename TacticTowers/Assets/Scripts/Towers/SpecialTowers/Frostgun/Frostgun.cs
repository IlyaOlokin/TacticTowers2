using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frostgun : Tower
{
    public float freezeTime;
    public float freezeTimeMultiplier;
    public float freezeStacksPerHit;
    public float freezeStacksPerHitMultiplier;
    [SerializeField] private GameObject frostBox;

    public int freezeStacksNeeded;
    private GameObject currentEnemy;
    private GameObject activeFrostBox;
    private float defaultFrostBoxWidth;
    [SerializeField] private GameObject frostEffect;
    
    [SerializeField] private Transform frostStartPos;
    
    [NonSerialized] public bool hasWidthUpgrade;

    [Header("Width Upgrade")] 
    [SerializeField] private float widthMultiplier;
    
    private void Start() => audioSrc = GetComponent<AudioSource>();

    new void Update() => base.Update();

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null)
        {
            DestroyFrostBox();
            currentEnemy = null;
            frostEffect.SetActive(false);
            audioSrc.Stop();
            return;
        }
        LootAtTarget(enemy);

        if (enemy != currentEnemy)
        {
            DestroyFrostBox();
            audioSrc.Stop();
        }
        
        if (shootDelayTimer <= 0)
        {
            if (enemy != currentEnemy)
            {
                activeFrostBox = Instantiate(frostBox, transform.position, towerCanon.transform.rotation);

                FrostBox frostBoxComponent = activeFrostBox.GetComponent<FrostBox>();
                frostBoxComponent.dmg = GetDmg();
                frostBoxComponent.attackSpeed = GetAttackSpeed();
                frostBoxComponent.freezeTime = freezeTime * freezeTimeMultiplier;
                frostBoxComponent.freezeStacksPerHit = GetFreezeStacksPerHit();
                frostBoxComponent.freezeStacksNeeded = freezeStacksNeeded;
                
                defaultFrostBoxWidth = activeFrostBox.transform.localScale.x ;
                currentEnemy = enemy;
                
                audioSrc.Play();
                frostEffect.SetActive(true);
            }
            
            shootDelayTimer = 1f / GetAttackSpeed();
        }

        if (activeFrostBox != null)
        {
            var frostDistance = GetFrostDistance(enemy);
            activeFrostBox.transform.position = (towerCanon.transform.up * frostDistance + frostStartPos.position + transform.position) / 2f;
            activeFrostBox.transform.localScale = new Vector3(defaultFrostBoxWidth * GetWidthMultiplier(), defaultFrostBoxWidth * 2.5f * frostDistance / 3f);

            activeFrostBox.transform.rotation = towerCanon.transform.rotation;
        }
    }
    
    private float GetFrostDistance(GameObject enemy)
    {
        if(CheckWallCollision(transform.position, enemy.transform.position, GetShootDistance(), true) != null)
        {
            var fireDistance = Vector2.Distance(transform.position,
                GetRayImpactPoint(transform.position, enemy.transform.position, true));
            return fireDistance;
        }
        
        return GetShootDistance();
    }

    private void DestroyFrostBox()
    {
        if (activeFrostBox != null) activeFrostBox.GetComponent<FrostBox>().DestroySelf(1f);
        activeFrostBox = null;
    }

    private float GetFreezeStacksPerHit() => freezeStacksPerHit * freezeStacksPerHitMultiplier;

    private float GetWidthMultiplier() => hasWidthUpgrade ? widthMultiplier : 1;
}
