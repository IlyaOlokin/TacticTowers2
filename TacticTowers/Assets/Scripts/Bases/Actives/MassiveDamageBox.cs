using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MassiveDamageBox : MonoBehaviour
{
    [NonSerialized] private List<Component> enemies = new List<Component>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            enemies.Add(other.GetComponent("Enemy"));
    }

    public void DamageEnemy(float damage)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage, DamageType.Normal, enemy.GetComponent<Transform>().position);
            }
        }
        enemies.Clear();
    }

}
