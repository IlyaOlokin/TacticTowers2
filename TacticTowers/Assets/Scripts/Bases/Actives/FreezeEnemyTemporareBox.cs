using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeEnemyTemporareBox : MonoBehaviour
{
    private List<Enemy> enemiesInside = new List<Enemy>();

    [NonSerialized] public float freezeTime;
    [NonSerialized] public float freezeStacksPerHit;

    [SerializeField] private GameObject freezeEffect;
    [NonSerialized] public int freezeStacksNeeded;

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

    private void Freeze(GameObject enemy)
    {
        if (!enemy.GetComponent<Freeze>())
        {
            enemy.transform.gameObject.AddComponent<Freeze>();
            enemy.GetComponent<Freeze>().freezeStacksNeeded = freezeStacksNeeded;
            enemy.GetComponent<Freeze>().freezeTime = freezeTime;
            enemy.GetComponent<Freeze>().freezeEffect = freezeEffect;
            enemy.GetComponent<Freeze>().freezeStacksPerHt = freezeStacksPerHit;
            enemy.GetComponent<Freeze>().FindEnemy();
            enemy.GetComponent<Freeze>().GetFreezeStack();
        }
        else if (!enemy.GetComponent<Freeze>().frozen)
        {
            enemy.GetComponent<Freeze>().GetFreezeStack();
        }
    }

    public void FreezeEnemy()
    {
        foreach (var enemy in enemiesInside)
        {
            Freeze(enemy.gameObject);
        }
    }
}
