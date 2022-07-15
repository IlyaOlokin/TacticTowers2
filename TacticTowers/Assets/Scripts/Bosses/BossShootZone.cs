using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootZone : MonoBehaviour
{
    private Boss boss;

    void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            boss.Shoot(collision.gameObject);
        }
    }
}
