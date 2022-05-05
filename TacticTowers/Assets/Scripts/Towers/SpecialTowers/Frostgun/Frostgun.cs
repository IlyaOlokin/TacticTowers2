using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frostgun : Tower
{
    public float freezeTime;
    [SerializeField] private GameObject frostBox;

    [SerializeField] private int freezeStacksNeeded;
    private GameObject currentEnemy;
    private GameObject activeFrostBox;
    private ParticleSystem ps;

    private void Start()
    { 
        ps = GetComponent<ParticleSystem>();
    }

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
                activeFrostBox = Instantiate(frostBox, transform.position, transform.rotation);
                
                activeFrostBox.GetComponent<FrostBox>().dmg = GetDmg();
                activeFrostBox.GetComponent<FrostBox>().attackSpeed = GetAttackSpeed();
                activeFrostBox.GetComponent<FrostBox>().freezeTime = freezeTime;
                activeFrostBox.GetComponent<FrostBox>().freezeStacksNeeded = freezeStacksNeeded;
                
                activeFrostBox.GetComponent<FrostBox>().frostStartPos = transform.position;
                activeFrostBox.transform.localScale = new Vector3(activeFrostBox.transform.localScale.x, GetShootDistance());
                activeFrostBox.transform.position = ((transform.up * GetShootDistance() + transform.position) + transform.position) / 2f;
                currentEnemy = enemy;
            }
            
            shootDelayTimer = 1f / GetAttackSpeed();
        }

        if (activeFrostBox != null)
        {
            activeFrostBox.transform.position = ((transform.up * GetShootDistance() + transform.position) + transform.position) / 2f;
            activeFrostBox.transform.rotation = transform.rotation;
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
}
