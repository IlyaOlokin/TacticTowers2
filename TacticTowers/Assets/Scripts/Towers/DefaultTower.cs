using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTower : Tower
{
    [SerializeField] private GameObject bullet;
    
    [SerializeField] private float bulletSpeed;
    
    void Update()
    {
        base.Update();
    }
    
    protected override void Shoot(GameObject enemy)
    {
        Vector3 dir = transform.position - enemy.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle + 90);
        if (shootDelayTimer <= 0)
        {
            shootDelayTimer = 1f / attackSpeed;
            var newBullet =  Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Bullet>().Dmg = Dmg;
            newBullet.GetComponent<Bullet>().Speed = bulletSpeed;
            newBullet.GetComponent<Bullet>().OnCreate();
        }
    }
}
