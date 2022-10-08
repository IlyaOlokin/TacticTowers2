using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowingDownIncreasingHpBox : MonoBehaviour
{
    [NonSerialized] public float SlowihgDownSpeed = 1;
    [NonSerialized] public float IncreasingHp = 1;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent("Enemy");
            enemy.GetComponent<NavMeshAgent>().speed /= SlowihgDownSpeed;
            enemy.GetComponent<Enemy>().hp *= IncreasingHp;
        }

    }
}
