using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shotgun : Tower
{
    [SerializeField] private GameObject bullet;
    
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float angleBetweenBullets;
    public int bulletCount;
    public int bonusBullets;

    [NonSerialized] public bool hasDoubleDamageUpgrade;

    [Header("Double Damage Upgrade")] 
    [SerializeField] private float doubleDamageChance;
    
    
    [NonSerialized] public bool hasDoubleShotUpgrade;
    
    [Header("Double Damage Upgrade")]
    [SerializeField] private float doubleShotChance;
    [SerializeField] private float doubleShotDelay;
    
    private void Start() => audioSrc = GetComponent<AudioSource>();
    private new void Update() => base.Update();

    protected override void Shoot(GameObject enemy)
    {
        if (enemy == null) return;

        LootAtTarget(enemy.transform.position);


        if (shootDelayTimer <= 0)
        {
            int shotsCount = 1;
            if (hasDoubleShotUpgrade && Random.Range(0f, 1f) < doubleShotChance) shotsCount = 2;
            
            StartCoroutine(MakeMultipleShots(shotsCount, doubleShotDelay));

            shootDelayTimer = 1f / GetAttackSpeed();
        }
    }

    private void MakeShot()
    {
        for (var i = 1; i <= bulletCount + bonusBullets; i++)
        {
            var towerRot = towerCanon.transform.eulerAngles;
            var bulletAngle = 0f;

            if ((bulletCount + bonusBullets) % 2 == 1)
                bulletAngle = i / 2 * angleBetweenBullets * (i % 2 == 0 ? 1 : -1f);
            else
                bulletAngle = i / 2 * angleBetweenBullets * (i % 2 == 0 ? 1 : -1f) -
                              angleBetweenBullets / 2f;

            var newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, bulletAngle + towerRot.z));
            var bulletComponent = newBullet.GetComponent<Bullet>();
            bulletComponent.Dmg = GetDmg() * GetLocalDamageMultiplier();
            bulletComponent.Speed = bulletSpeed;
            bulletComponent.enemiesToIgnore = enemiesToIgnore;
            bulletComponent.departurePos = transform.position;
        }
        audioSrc.PlayOneShot(audioSrc.clip);
    }

    private IEnumerator MakeMultipleShots(int shotsCount, float delayInSeconds)
    {
        for (int i = 0; i < shotsCount; i++)
        {
            MakeShot();
            yield return new WaitForSeconds(delayInSeconds);
        }
    }

    private float GetLocalDamageMultiplier()
    {
        if (hasDoubleDamageUpgrade && Random.Range(0f, 1f) < doubleDamageChance)
            return 2f;
        
        return 1f;
    }
}