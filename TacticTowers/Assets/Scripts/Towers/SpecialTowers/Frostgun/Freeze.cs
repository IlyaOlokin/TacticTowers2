using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Freeze : MonoBehaviour
{
    private static bool IsGlobalBonusDamageActivated;
    private static float GlobalFrozenDamageMultiplier;
    
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
        enemy.TakeSlow(0.25f, 5f);
        //enemy.TakeSlow(currentSpeed => currentSpeed - freezeStacks / freezeStacksNeeded * currentSpeed * 0.5f, 2f);
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
        //enemy.StopCoroutine(nameof(enemy.BeSlowed));
        //enemy.TakeSlow(1, freezeTime);
        //!!!!!!!!!!enemy.GetComponent<NavMeshAgent>().speed = 0;
        frozen = true;
        StopAllCoroutines();
        enemy.TakeStun(freezeTime, 2f);
        
        newFreezeEffect = Instantiate(EnemyVFXManager.Instance.GetEffect("FreezeOnEnemy").effect, transform.position, Quaternion.identity, enemy.transform);
        StartCoroutine(Unfreeze(freezeTime));
    }

    IEnumerator LoseFreezeStack()
    {
        yield return new WaitForSeconds(5f);
        freezeStacks -= freezeStacksPerHt;
        ColorEnemy();
        enemy.TakeSlow(0.25f, 5f);
        //enemy.TakeSlow(currentSpeed => currentSpeed - freezeStacks / freezeStacksNeeded * currentSpeed * 0.5f, 2f);
        //SlowEnemy();
    }
    
    IEnumerator Unfreeze(float freezeTime)
    {
        //!!!!!!!!!!!enemy.GetComponent<NavMeshAgent>().speed = enemySpeed;
        //enemy.StopCoroutine(nameof(enemy.BeSlowed));
        yield return new WaitForSeconds(freezeTime);
        Destroy(newFreezeEffect.gameObject);
        StopAllCoroutines();
        freezeStacks = 0;
        ColorEnemy();
    }

    public void UnfreezeInstantly()
    {
        //!!!!!!!!!!!!!enemy.GetComponent<NavMeshAgent>().speed = enemySpeed;
        //enemy.StopCoroutine(nameof(enemy.BeSlowed));
        enemy.TakeSlow(0f, 0f);
        Destroy(newFreezeEffect?.gameObject);
        StopAllCoroutines();
        freezeStacks = 0;
        ColorEnemy();
    }
    
    private void ColorEnemy()
    {
        //enemy.GetComponent<SpriteRenderer>().color = new Color(enemyColor.r,
       //                                                         enemyColor.g,
        //                                                        enemyColor.b + (freezeStacks / freezeStacksNeeded) * 0.3f);
    }
    
    public static void ResetGlobalFrozenDamageMultiplier()
    {
        GlobalFrozenDamageMultiplier = 1;
    }
    
    public static void MultiplyGlobalFrozenMultiplier(float multiplier)
    {
        GlobalFrozenDamageMultiplier *= multiplier;
    }

    public static float GetGlobalFrozenMultiplier()
    {
        return GlobalFrozenDamageMultiplier;
    }
    
    public static void SetActiveFrozenDamageMultiplier(bool enabled)
    {
        IsGlobalBonusDamageActivated = enabled;
    }
    
    public static bool GetActiveFrozenDamageMultiplier()
    {
        return IsGlobalBonusDamageActivated;
    }
}
