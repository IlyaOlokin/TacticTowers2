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
    [NonSerialized] private float period;
    [NonSerialized] public float duration;

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
        if (Input.GetMouseButton(1))
        {
            gameObject.SetActive(false);
        }
        if (Input.GetMouseButton(0))
        {
            isActive = true;
            FunctionTimer.Create(Off, duration);
            GameObject.FindGameObjectWithTag("Base").GetComponent<Base>().UpdateAbilityTimer();
        }
        if (!isActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetMousePosition(), 100f);
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
        gameObject.SetActive(false);
    }

    private Vector3 GetMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
