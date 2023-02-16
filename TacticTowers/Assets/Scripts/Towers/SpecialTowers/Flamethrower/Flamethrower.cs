using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Tower
{
    public float burnDmg;
    public float burnDmgMultiplier;
    public float burnTime;
    public float burnTimeMultiplier;
    [SerializeField] private GameObject flameBox;
    [SerializeField] private Transform flameStartPos;
    
    private GameObject currentEnemy;
    private GameObject activeFlameBox;
    private float defaultFlameBoxWidth;
    
    [NonSerialized] public bool hasWidthUpgrade;

    [Header("Width Upgrade")] 
    [SerializeField] private float widthMultiplier;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private new void Update() => base.Update();

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null)
        {
            DestroyFlameBox();
            currentEnemy = null;
            audioSrc.Stop();

            return;
        }
        LootAtTarget(enemy);

        if (enemy != currentEnemy)
        {
            DestroyFlameBox();
            audioSrc.Stop();

        }
        
        if (shootDelayTimer <= 0)
        {
            if (enemy != currentEnemy)
            {
                activeFlameBox = Instantiate(flameBox, transform.position, towerCanon.transform.rotation);
                activeFlameBox.GetComponent<FlameBox>().dmg = GetDmg();
                activeFlameBox.GetComponent<FlameBox>().attackSpeed = GetAttackSpeed();
                activeFlameBox.GetComponent<FlameBox>().burnDmg = GetBurnDmg();
                activeFlameBox.GetComponent<FlameBox>().burnTime = burnTime * burnTimeMultiplier;
                
                defaultFlameBoxWidth = activeFlameBox.transform.localScale.x ;
                currentEnemy = enemy;
                
                audioSrc.Play();
            }
            
            shootDelayTimer = 1f / GetAttackSpeed();
        }

        if (activeFlameBox != null)
        {
            var fireDistance = GetFireDistance(enemy);
            activeFlameBox.transform.position = ((towerCanon.transform.up * fireDistance + flameStartPos.position) + transform.position) / 2f;
            activeFlameBox.transform.localScale = new Vector3(defaultFlameBoxWidth * GetWidthMultiplier(), defaultFlameBoxWidth * 2.5f * fireDistance / 3f);

            activeFlameBox.transform.rotation = towerCanon.transform.rotation;
        }
    }

    private float GetFireDistance(GameObject enemy)
    {
        if(CheckWallCollision(transform.position, enemy.transform.position, GetShootDistance(), true) != null)
        {
            var fireDistance = Vector2.Distance(transform.position,
                GetRayImpactPoint(transform.position, enemy.transform.position, true));
            return fireDistance;
        }
        
        return GetShootDistance();
    }

    private void DestroyFlameBox()
    {
        if (activeFlameBox != null) activeFlameBox.GetComponent<FlameBox>().DestroySelf(1f);
        activeFlameBox = null;
    }

    private float GetBurnDmg() => burnDmg * burnDmgMultiplier;

    private float GetWidthMultiplier() => hasWidthUpgrade ? widthMultiplier : 1;
}
