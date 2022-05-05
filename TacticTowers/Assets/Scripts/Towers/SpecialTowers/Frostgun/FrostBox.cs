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


    [NonSerialized] public ParticleSystem ps;
    [NonSerialized] public Vector3 frostStartPos;
    [NonSerialized] public float freezeTime;

    [SerializeField] private GameObject freezeEffect;

    private void Start()
    {
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        ps.transform.position = frostStartPos;
        var mainModule = ps.main;
        mainModule.startLifetime = transform.localScale.y / 3f;
    }


    protected void Update()
    {
        if (dmgDelayTimer > 0) dmgDelayTimer -= Time.deltaTime;

        if (dmgDelayTimer <= 0) DealDamage();
    }
    
    private void DealDamage()
    {
        for (var index = 0; index < enemiesInside.Count; index++)
        {
            var enemy = enemiesInside[index];
            enemy.TakeDamage(dmg);
            Freeze(enemy.gameObject);
            
        }

        dmgDelayTimer = 1f / attackSpeed;
    }

    private void Freeze(GameObject enemy)
    {
        if (!enemy.GetComponent<Freeze>())
        {
            enemy.transform.gameObject.AddComponent<Freeze>();
            enemy.GetComponent<Freeze>().freezeStacksNeeded = freezeStacksNeeded;
            enemy.GetComponent<Freeze>().freezeTime = freezeTime;
            enemy.GetComponent<Freeze>().freezeEffect = freezeEffect;
            enemy.GetComponent<Freeze>().FindEnemy();
            enemy.GetComponent<Freeze>().GetFreezeStack();
        }
        else if (!enemy.GetComponent<Freeze>().frozen)
        {
            enemy.GetComponent<Freeze>().GetFreezeStack();
        }
    }

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
