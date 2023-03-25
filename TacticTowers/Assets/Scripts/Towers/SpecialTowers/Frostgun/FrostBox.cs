using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBox : MonoBehaviour
{
    private List<Enemy> enemiesInside = new List<Enemy>();

    [NonSerialized] public float dmg;
    [NonSerialized] public float attackSpeed;
    [NonSerialized] public int freezeStacksNeeded;

    private float dmgDelayTimer;
    private Material myMaterial;

    private float appearTimer;
    private float appearDelay = 2f;

    private float destroyTimer;
    private float destroyDelay;
    
    private bool needToBeDestroyed;
    
    private DamageType damageType = DamageType.Normal;

    [NonSerialized] public ParticleSystem ps;
    [NonSerialized] public Vector3 frostStartPos;
    [NonSerialized] public float freezeTime;
    [NonSerialized] public float freezeStacksPerHit;
    [NonSerialized] public bool hasImmuneIgnoreUpgrade;

    //[SerializeField] private GameObject freezeEffect;
    private static readonly int StartFlame = Shader.PropertyToID("_StartFlame");
    private static readonly int EndFlame = Shader.PropertyToID("_EndFlame");

    private void Start()
    {
        myMaterial = GetComponent<SpriteRenderer>().material;
        myMaterial.SetFloat(StartFlame, 0);
    }
    
    protected void Update()
    {
        if (dmgDelayTimer > 0) dmgDelayTimer -= Time.deltaTime;

        if (dmgDelayTimer <= 0) DealDamage();
        if (needToBeDestroyed)
        {
            destroyTimer += Time.deltaTime;
            myMaterial.SetFloat(EndFlame, destroyTimer / destroyDelay);
        }

        if (appearTimer < appearDelay)
        {
            appearTimer += Time.deltaTime;
            myMaterial.SetFloat(StartFlame, appearTimer / appearDelay);
        }
    }
    
    private void DealDamage()
    {
        for (var index = 0; index < enemiesInside.Count; index++)
        {
            var enemy = enemiesInside[index];
            enemy.TakeDamage(dmg, damageType, transform.position);
            //Freeze(enemy.gameObject);
            enemy.TakeFreeze(new FreezeStats(freezeStacksNeeded, freezeTime, freezeStacksPerHit), hasImmuneIgnoreUpgrade);
        }

        dmgDelayTimer = 1f / attackSpeed;
    }
    
    public void DestroySelf(float delay)
    {
        Destroy(gameObject, delay);
        destroyDelay = delay;
        needToBeDestroyed = true; 
    }
/*
    private void Freeze(GameObject enemy)
    {
        if (!enemy.GetComponent<Freeze>())
        {
            enemy.transform.gameObject.AddComponent<Freeze>();
            enemy.GetComponent<Freeze>().freezeStacksNeeded = freezeStacksNeeded;
            enemy.GetComponent<Freeze>().freezeTime = freezeTime;
            enemy.GetComponent<Freeze>().freezeEffect = freezeEffect;
            enemy.GetComponent<Freeze>().freezeStacksPerHt = freezeStacksPerHit;
            enemy.GetComponent<Freeze>().GetFreezeStack();
        }
        else if (!enemy.GetComponent<Freeze>().frozen)
        {
            enemy.GetComponent<Freeze>().GetFreezeStack();
        }
    }
*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (!enemiesInside.Contains(enemy))
            {
                enemiesInside.Add(enemy);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemiesInside.Contains(enemy))
            {
                enemiesInside.Remove(enemy);
            }
            
        }
    }
}
