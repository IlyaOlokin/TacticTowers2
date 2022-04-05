using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTower : Tower
{
    [SerializeField] private GameObject bullet;
    
    [SerializeField] private float bulletSpeed;
    private float shootDelayTimer;

    

    void Update()
    {
        base.Update();
        
        if (shootDelayTimer <= 0) shootDelayTimer = 0;
        else shootDelayTimer -= Time.deltaTime;
    }
    
    protected override void Shoot(GameObject enemy)
    {
        Vector3 dir = transform.position - enemy.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle + 90);
        if (shootDelayTimer == 0)
        {
            shootDelayTimer += shootDelay;
            var newBullet =  Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Bullet>().Dmg = Dmg;
            newBullet.GetComponent<Bullet>().Speed = bulletSpeed;
            newBullet.GetComponent<Bullet>().OnCreate();
        }
    }
}
