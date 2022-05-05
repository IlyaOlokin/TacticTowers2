using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Freeze : MonoBehaviour
{
    private int freezeStacks;
    [NonSerialized] public int freezeStacksNeeded;
    [NonSerialized] public float freezeTime;
    [NonSerialized] public bool frozen;

    [NonSerialized] public GameObject freezeEffect;
    private GameObject newFreezeEffect;
    
    private Enemy enemy;
    private float enemySpeed;
    private Color enemyColor;
    

    public void FindEnemy()
    {
        enemy = GetComponent<Enemy>();
        enemySpeed = enemy.GetComponent<NavMeshAgent>().speed;
        enemyColor = enemy.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (freezeStacks <= 0)
        {
            Destroy(this);
        }
        
        if (freezeStacks >= freezeStacksNeeded && !frozen)
        {
            FreezeEnemy();
        }
    }

    public void GetFreezeStack()
    {
        freezeStacks += 1;
        
        ColorEnemy();

        StartCoroutine(LoseFreezeStack());
    }
    
    private void FreezeEnemy()
    {
        frozen = true;
        enemy.GetComponent<NavMeshAgent>().speed = 0;
        newFreezeEffect = Instantiate(freezeEffect, transform.position, Quaternion.identity, enemy.transform);
        StartCoroutine(Unfreeze());
    }

    IEnumerator LoseFreezeStack()
    {
        yield return new WaitForSeconds(5f);
        freezeStacks -= 1;
        ColorEnemy();
    }
    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(freezeTime);
        enemy.GetComponent<NavMeshAgent>().speed = enemySpeed;
        Destroy(newFreezeEffect.gameObject);
        StopAllCoroutines();
        freezeStacks = 0;
        ColorEnemy();
    }
    
    private void ColorEnemy()
    {
        enemy.GetComponent<SpriteRenderer>().color = new Color(enemyColor.r,
                                                                enemyColor.g,
                                                                enemyColor.b + (freezeStacks / (float) freezeStacksNeeded) * 0.3f);
    }
}
