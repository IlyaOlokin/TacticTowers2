using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Tower
{
    [SerializeField] private float burnDmg;
    public float burnDmgMultiplier;
    public float burnTime;
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
                activeFlameBox.GetComponent<FlameBox>().burnTime = burnTime;
                
                activeFlameBox.GetComponent<FlameBox>().flameStartPos = flameStartPos.position;
                activeFlameBox.transform.localScale = new Vector3(activeFlameBox.transform.localScale.x, GetShootDistance());
                activeFlameBox.transform.position = ((transform.up * GetShootDistance() + transform.position) + transform.position) / 2f;
                currentEnemy = enemy;
                
                shooting = true;

            }
            
            shootDelayTimer = 1f / GetAttackSpeed();
        }

        if (activeFlameBox != null)
        {
            activeFlameBox.transform.position = ((towerCanon.transform.up * GetShootDistance() + transform.position) + transform.position) / 2f;
            activeFlameBox.transform.rotation = towerCanon.transform.rotation;
        }
    }

    private void DestroyFlameBox()
    {
        Destroy(activeFlameBox, GetShootDistance() / 3f);
        if (activeFlameBox != null && activeFlameBox.GetComponent<FlameBox>() != null)
        {
            activeFlameBox.GetComponent<FlameBox>().ps.Stop();
        }

        activeFlameBox = null;
    }

    private float GetBurnDmg()
    {
        return burnDmg * burnDmgMultiplier;
    }
}
