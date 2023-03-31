using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Freeze : MonoBehaviour
{
    private float freezeStacks;
    [NonSerialized] public int freezeStacksNeeded;
    [NonSerialized] public float freezeTime;
    [NonSerialized] public float freezeStacksPerHt;
    [NonSerialized] public bool frozen;

    private GameObject newFreezeEffect;
    
    [SerializeField] private Enemy enemy;
    private float enemySpeed;
    private Color enemyColor;

    public void OnEnable()
    {
        enemy = GetComponent<Enemy>();
        // enemySpeed = enemy.GetComponent<NavMeshAgent>().speed;
        enemyColor = enemy.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (freezeStacks <= 0)
            Destroy(this);

        if (freezeStacks >= freezeStacksNeeded && !frozen)
            FreezeEnemy();
    }

    public void GetFreezeStack()
    {
        freezeStacks += freezeStacksPerHt;
        
        ColorEnemy();
        enemy.TakeSlow(currentSpeed => currentSpeed - freezeStacks / freezeStacksNeeded * currentSpeed * 0.5f, 5f);
        //SlowEnemy();
        
        if (!frozen) 
            StartCoroutine(LoseFreezeStack());
    }

    private void SlowEnemy()
    {
        enemy.GetComponent<NavMeshAgent>().speed = enemySpeed - freezeStacks / freezeStacksNeeded * enemySpeed * 0.5f;
    }

    private void FreezeEnemy()
    {
        frozen = true;
        //enemy.StopCoroutine(nameof(enemy.BeSlowed));
        StopAllCoroutines();
        //enemy.TakeSlow(1, freezeTime);
        enemy.TakeStun(freezeTime, true);
        //!!!!!!!!!!enemy.GetComponent<NavMeshAgent>().speed = 0;
        
        newFreezeEffect = Instantiate(EnemyVFXManager.Instance.GetEffect("FreezeOnEnemy").effect, transform.position, Quaternion.identity, enemy.transform);
        StartCoroutine(Unfreeze(freezeTime));
    }

    IEnumerator LoseFreezeStack()
    {
        yield return new WaitForSeconds(5f);
        freezeStacks -= freezeStacksPerHt;
        ColorEnemy();
        enemy.TakeSlow(currentSpeed => currentSpeed - freezeStacks / freezeStacksNeeded * currentSpeed * 0.5f, 5f);
        //SlowEnemy();
    }
    
    IEnumerator Unfreeze(float freezeTime)
    {
        yield return new WaitForSeconds(freezeTime);
        //!!!!!!!!!!!enemy.GetComponent<NavMeshAgent>().speed = enemySpeed;
        Destroy(newFreezeEffect.gameObject);
        //enemy.StopCoroutine(nameof(enemy.BeSlowed));
        StopAllCoroutines();
        freezeStacks = 0;
        ColorEnemy();
    }

    public void UnfreezeInstantly()
    {
        enemy.TakeSlow(0f, 0f);
        //!!!!!!!!!!!!!enemy.GetComponent<NavMeshAgent>().speed = enemySpeed;
        Destroy(newFreezeEffect?.gameObject);
        //enemy.StopCoroutine(nameof(enemy.BeSlowed));
        StopAllCoroutines();
        freezeStacks = 0;
        ColorEnemy();
    }
    
    private void ColorEnemy()
    {
        enemy.GetComponent<SpriteRenderer>().color = new Color(enemyColor.r,
                                                                enemyColor.g,
                                                                enemyColor.b + (freezeStacks / freezeStacksNeeded) * 0.3f);
    }
}
