using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneBox : MonoBehaviour
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

    private void OnTriggerStay2D(Collider2D other)
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


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isActive = true;
        }
        if (!isActive)
        {
            transform.position = GetMousePosition();
        }
        else
        {
            if (periodBetweenDmg > 0) periodBetweenDmg -= Time.deltaTime;

            if (periodBetweenDmg <= 0)
            {
                PeriodDamage();
                periodBetweenDmg = period;
                enemies.Clear();
            }
        }
    }

    private void PeriodDamage()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damage, DamageType.Normal, transform.position);
            }
        }
    }

    public void Off()
    {
        isActive = false;
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
