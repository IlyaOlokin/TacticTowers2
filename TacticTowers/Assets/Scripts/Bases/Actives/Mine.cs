using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mine : MonoBehaviour
{
    [NonSerialized] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private float damage;
    [SerializeField] private float duration;
    private bool isActive = false;
    private bool needActivate = false;
    private bool beenActivated = false;
    public Vector3 targetPos;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosionEffect;
    private AudioSource audioSrc;
    
    private void Start()
    {
        speed *= Random.Range(0.95f, 1.05f);
        audioSrc = GetComponent<AudioSource>();
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
        if (!beenActivated)
        {
            if (Vector3.Distance(transform.position, targetPos) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
            else
            {
                needActivate = true;
                beenActivated = true;
                //audioSrc.PlayOneShot(audioSrc.clip);
            }
        }
        
        if (enemies.Count >= 1 && needActivate)
        {
            isActive = true;
            needActivate = false;
        }

        if (isActive)
        {
            isActive = false;
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        yield return new WaitForSeconds(duration);
        for (var i = 0; i < enemies.Count; i++)
        {
            var enemy = enemies[i];
            if (enemy != null)
            {
                enemy.TakeDamage(damage, DamageType.Normal, transform.position);
            }
        }
        var newExplosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        newExplosion.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        Destroy(gameObject);
    }
}
