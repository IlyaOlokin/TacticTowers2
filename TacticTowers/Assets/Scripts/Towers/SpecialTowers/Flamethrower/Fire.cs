using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Fire : MonoBehaviour
{
    private static float GlobalBurnMultiplier = 1;
    
    public float burnTime;
    public float burnDmg;
    //[NonSerialized] public GameObject fire;
    private GameObject newFire;
    private Enemy enemy;
    
    private float dmgDelayTimer;
    private float dmgDelay = 0.5f;
    private DamageType damageType = DamageType.Fire;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        newFire = Instantiate(EnemyVFXManager.Instance.GetEffect("FireOnEnemy").effect, transform.position, Quaternion.identity, enemy.transform);
    }

    void Update()
    {
        burnTime -= Time.deltaTime;
        if (burnTime <= 0)
        {
            Destroy(this);
        }
        
        if (dmgDelayTimer > 0) dmgDelayTimer -= Time.deltaTime;
        
        if (dmgDelayTimer <= 0) DealDamage();
    }

    public static void ResetGlobalBurnMultiplier()
    {
        GlobalBurnMultiplier = 1;
    }
    
    public static void MultiplyGlobalBurnMultiplier(float multiplier)
    {
        GlobalBurnMultiplier *= multiplier;
    }

    private void DealDamage()
    {
        enemy.TakeDamage(burnDmg * GlobalBurnMultiplier, damageType, transform.position);
        dmgDelayTimer = dmgDelay;
    }

    private void OnDestroy()
    {
        if (newFire != null)
            Destroy(newFire.gameObject);
    }
    
    
}
