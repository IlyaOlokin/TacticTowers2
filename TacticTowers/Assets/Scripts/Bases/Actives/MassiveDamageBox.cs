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
        if (other.transform.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
    }

    public void DamageEnemy(float damage)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage, DamageType.Normal, transform.position);
            }
        }
        enemies.Clear();
    }

}
