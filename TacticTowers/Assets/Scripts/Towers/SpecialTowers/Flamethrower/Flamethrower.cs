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
    private ParticleSystem ps;
    [NonSerialized] public bool shooting;

    private void Start()
    { 
        ps = GetComponent<ParticleSystem>();
        AudioManager.Instance.flamethrowers.Add(this);
    }

    void Update()
    {
        base.Update();
    }
    
    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null)
        {
            DestroyFlameBox();
            currentEnemy = null;
            shooting = false;


            return;
        }
        LootAtTarget(enemy);

        if (enemy != currentEnemy)
        {
            DestroyFlameBox();
            shooting = false;


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
                
                //activeFlameBox.GetComponent<FlameBox>().flameStartPos = flameStartPos.position;
                //activeFlameBox.transform.localScale = new Vector3(activeFlameBox.transform.localScale.x, GetShootDistance());
                var fireDistance = GetFireDistance(enemy);
                activeFlameBox.transform.localScale = new Vector3(activeFlameBox.transform.localScale.x, activeFlameBox.transform.localScale.x * 2.5f * fireDistance / 3f);
                activeFlameBox.transform.position = ((transform.up * fireDistance + transform.position) + flameStartPos.position) / 2f;
                currentEnemy = enemy;
                
                shooting = true;

            }
            
            shootDelayTimer = 1f / GetAttackSpeed();
        }

        if (activeFlameBox != null)
        {
            var fireDistance = GetFireDistance(enemy);
            activeFlameBox.transform.position = ((towerCanon.transform.up * fireDistance + flameStartPos.position) + transform.position) / 2f;
            activeFlameBox.transform.localScale = new Vector3(activeFlameBox.transform.localScale.x, activeFlameBox.transform.localScale.x * 2.5f * fireDistance / 3f);

            activeFlameBox.transform.rotation = towerCanon.transform.rotation;
        }
    }

    private float GetFireDistance(GameObject enemy)
    {
        if(CheckWallCollision(transform.position, enemy.transform.position, true) != null)
        {
            var fireDistance = Vector2.Distance(transform.position,
                GetRayImpactPoint(transform.position, enemy.transform.position, true));
            return fireDistance;
        }
        else
        {
            return GetShootDistance();
        }
    }

    private void DestroyFlameBox()
    {
        if (activeFlameBox != null) activeFlameBox.GetComponent<FlameBox>().DestroySelf(1f);
        activeFlameBox = null;
    }

    private float GetBurnDmg()
    {
        return burnDmg * burnDmgMultiplier;
    }
}
