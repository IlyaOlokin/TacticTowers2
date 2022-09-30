using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Tower
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private SpriteRenderer heatIndicator;

    private float heatCount = 0;
    private float coolTimer;

    [SerializeField] public int maxHeat;
    [SerializeField] public float bonusAttackSpeedPerHeat;
    [SerializeField] public float coolDelay;

    void Update()
    {
        base.Update();

        if (coolTimer < 0)
        {
            if (heatCount > 0) heatCount -= Time.deltaTime * 2;
            else heatCount = 0;
        }
        else coolTimer -= Time.deltaTime;

        BarrelHeat();
    }

    private void BarrelHeat()
    {
        var color = heatIndicator.color;
        color = Color.HSVToRGB(0, heatCount / maxHeat * 0.6f , 1);
        heatIndicator.color = color;
    }

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;

        LootAtTarget(enemy);

        if (shootDelayTimer <= 0)
        {
            heatCount = Mathf.Ceil(heatCount);
            shootDelayTimer = 1f / (GetAttackSpeed() + heatCount * bonusAttackSpeedPerHeat);
            if (heatCount < maxHeat) heatCount += 1;
            coolTimer = coolDelay;
            var newBullet = Instantiate(bullet, transform.position, towerCanon.transform.rotation);
            var bulletComponent = newBullet.GetComponent<Bullet>();
            bulletComponent.Dmg = GetDmg();
            bulletComponent.Speed = bulletSpeed;
            bulletComponent.enemiesToIgnore = enemiesToIgnore;
            
            AudioManager.Instance.Play("MinigunShot");
        }
    }
}