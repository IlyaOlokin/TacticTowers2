using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frostgun : Tower
{
    public float freezeTime;
    [SerializeField] private float freezeStacksPerHit;
    public float freezeStacksPerHitMultiplier;
    [SerializeField] private GameObject frostBox;

    [SerializeField] private int freezeStacksNeeded;
    private GameObject currentEnemy;
    private GameObject activeFrostBox;
    [SerializeField] private GameObject frostEffect;
    

    void Update()
    {
        base.Update();
    }
    
    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null)
        {
            DestroyFrostBox();
            currentEnemy = null;
            frostEffect.SetActive(false);
            return;
        }
        LootAtTarget(enemy);

        if (enemy != currentEnemy)
        {
            DestroyFrostBox();
        }
        
        if (shootDelayTimer <= 0)
        {
            if (enemy != currentEnemy)
            {
                activeFrostBox = Instantiate(frostBox, transform.position, towerCanon.transform.rotation);
                
                activeFrostBox.GetComponent<FrostBox>().dmg = GetDmg();
                activeFrostBox.GetComponent<FrostBox>().attackSpeed = GetAttackSpeed();
                activeFrostBox.GetComponent<FrostBox>().freezeTime = freezeTime;
                activeFrostBox.GetComponent<FrostBox>().freezeStacksPerHit = GetFreezeStacksPerHit();
                activeFrostBox.GetComponent<FrostBox>().freezeStacksNeeded = freezeStacksNeeded;
                
                activeFrostBox.GetComponent<FrostBox>().frostStartPos = transform.position;
                activeFrostBox.transform.localScale = new Vector3(activeFrostBox.transform.localScale.x, GetShootDistance());
                activeFrostBox.transform.position = ((transform.up * GetShootDistance() + transform.position) + transform.position) / 2f;
                currentEnemy = enemy;
                
                frostEffect.SetActive(true);
            }
            
            shootDelayTimer = 1f / GetAttackSpeed();
        }

        if (activeFrostBox != null)
        {
            activeFrostBox.transform.position = (towerCanon.transform.up * GetShootDistance() + transform.position + transform.position) / 2f;
            activeFrostBox.transform.rotation = towerCanon.transform.rotation;
        }
    }

    private void DestroyFrostBox()
    {
        Destroy(activeFrostBox, GetShootDistance() / 3f);
        if (activeFrostBox != null && activeFrostBox.GetComponent<FrostBox>() != null)
        {
            activeFrostBox.GetComponent<FrostBox>().ps.Stop();
        }

        activeFrostBox = null;
    }

    private float GetFreezeStacksPerHit()
    {
        return freezeStacksPerHit * freezeStacksPerHitMultiplier;
    }
}
