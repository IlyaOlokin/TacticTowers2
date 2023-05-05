using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MassiveDamageBox : MonoBehaviour
{
    [NonSerialized] public float damage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            DamageEnemy(enemy);
        }
    }

    private void DamageEnemy(Enemy enemy)
    {
        enemy.GetComponent<Enemy>().TakeDamage(damage, DamageType.Normal, transform.position);
    }

    public void Explode(float damage)
    {
        this.damage = damage;
        StartCoroutine(Deactivate(1f));
    }

    private IEnumerator Deactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

}
