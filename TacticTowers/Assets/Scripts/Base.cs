using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private float hp;

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
