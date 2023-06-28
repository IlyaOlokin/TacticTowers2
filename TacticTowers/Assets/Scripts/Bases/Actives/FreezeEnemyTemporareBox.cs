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

    private void Start()
    {
        StartCoroutine(Deactivate(1f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Freeze(other.gameObject.GetComponent<Enemy>());
        }
    }

    private void Freeze(Enemy enemy)
    {
        /*if (enemy.GetComponent<Freeze>())
        {
            enemy.GetComponent<Freeze>().UnfreezeInstantly();
            Destroy(enemy.GetComponent<Freeze>());
        }
        
        var componentFreeze = enemy.gameObject.AddComponent<Freeze>();

        componentFreeze.freezeStacksNeeded = freezeStacksNeeded;
        componentFreeze.freezeTime = freezeTime;
        //componentFreeze.freezeEffect = freezeEffect;
        componentFreeze.freezeStacksPerHt = freezeStacksPerHit;
        
        componentFreeze.GetFreezeStack();*/
        enemy.TakeFreeze(new FreezeStats(freezeStacksNeeded, freezeTime, freezeStacksPerHit), true);
    }
    
    private IEnumerator Deactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
