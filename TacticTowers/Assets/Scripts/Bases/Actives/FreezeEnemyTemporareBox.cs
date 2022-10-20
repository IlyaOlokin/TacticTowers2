using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeEnemyTemporareBox : MonoBehaviour
{
    [NonSerialized] private Dictionary<Component, float> enemies = new Dictionary<Component, float>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            enemies.Add(other.GetComponent("Enemy"), other.GetComponent("Enemy").GetComponent<NavMeshAgent>().speed);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) 
            enemies.Remove(other.GetComponent("Enemy"));
    }

    public void FreezeEnemy(float duration)
    {
        FunctionTimer.Create(GoBackToDefaultSpeed, duration);
        foreach (var enemy in enemies) 
            enemy.Key.GetComponent<NavMeshAgent>().speed = 0;
    }

    private void GoBackToDefaultSpeed()
    {
        foreach (var enemy in enemies) 
            enemy.Key.GetComponent<NavMeshAgent>().speed = enemy.Value;
    }
}
