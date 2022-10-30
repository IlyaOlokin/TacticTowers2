using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBox : MonoBehaviour
{
    private bool isActive = false;
    [NonSerialized] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private float damage;
    [SerializeField] private float periodBetweenDmg;
    private float period;

    private void Start()
    {
        period = periodBetweenDmg;
    }

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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }


    private void Update()
    {
        transform.position = GetMousePosition();

        if (periodBetweenDmg > 0) periodBetweenDmg -= Time.deltaTime;

        if (periodBetweenDmg <= 0)
        {
            PeriodDamage();
            periodBetweenDmg = period;
        }
    }

    private void PeriodDamage()
    {
        for (var i = 0; i < enemies.Count; i++)
        {
            var enemy = enemies[i];
            if (enemy != null)
            {
                enemy.TakeDamage(damage, DamageType.Normal, transform.position);
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
