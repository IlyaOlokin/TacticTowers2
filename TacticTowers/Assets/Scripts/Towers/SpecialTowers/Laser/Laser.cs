using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Tower
{
    [SerializeField] private GameObject laserBim;

    private float heatCount = 0;
    private float coolTimer;
    private List<GameObject> currentEnemies = new List<GameObject>() {null, null};
    private List<GameObject> activeLasers = new List<GameObject>() {null, null};

    [SerializeField] public int maxHeat;
    [SerializeField] public float maxHeatMultiplier;
    [SerializeField] public float multiplierPerHeatStack;
    [SerializeField] public float multiplierPerHeatStackMultiplier;
    [SerializeField] public float coolDelay;
    [SerializeField] public float coolDelayMultiplier;
    [SerializeField] private ContactFilter2D contactFilter;

    [NonSerialized] public bool shooting;
    private DamageType damageType = DamageType.Fire;

    [NonSerialized] public bool hasSecondLaserUpgrade;

    private void Start() => audioSrc = GetComponent<AudioSource>();

    private new void Update()
    {
        base.Update();

        if (coolTimer < 0)
        {
            if (heatCount > 0) heatCount -= Time.deltaTime * 2;
            else heatCount = 0;
        }
        else coolTimer -= Time.deltaTime;
    }
    
    protected override void Shoot(GameObject enemy)
    {
        LaserShoot(enemy, 0);
        LaserShoot(FindTarget(new List<GameObject>(){enemy}), 1);
        //if (hasSecondLaserUpgrade && heatCount >= maxHeat * maxHeatMultiplier) LaserShoot(FindTarget(new List<GameObject>(){enemy}), 1);
        DealDamage();
    }

    private void LaserShoot(GameObject target, int i)
    {
        if (target == null)
        {
            Destroy(activeLasers[i]);
            audioSrc.Stop();
            shooting = false;
            currentEnemies[i] = null;
            return;
        }

        LootAtTarget(target);

        if (target != currentEnemies[i])
        {
            Destroy(activeLasers[i]);
            audioSrc.Stop();
            shooting = false;
        }

        if (heatCount < maxHeat * maxHeatMultiplier) heatCount += Time.deltaTime;
        if (activeLasers[i] != null) activeLasers[i].GetComponent<LaserBeam>().IncreaseWidth(heatCount);
        if (target != currentEnemies[i])
        {
            activeLasers[i] = Instantiate(laserBim, transform.position, towerCanon.transform.rotation);
            activeLasers[i].GetComponent<LaserBeam>().target = target;
            activeLasers[i].GetComponent<LaserBeam>().origin = transform.position;
            currentEnemies[i] = target;
            audioSrc.Play();
            shooting = true;
        }
    }

    private void DealDamage()
    {
        if (shootDelayTimer <= 0)
        {
            shootDelayTimer = 1f / GetAttackSpeed();
            coolTimer = coolDelay * coolDelayMultiplier;

            for (var i = 0; i < currentEnemies.Count; i++)
            {
                if (currentEnemies[i] == null) continue;
                if (CheckWallCollision(transform.position, currentEnemies[i].transform.position, GetShootDistance(), false) is
                    null)
                {
                    if (currentEnemies[i].GetComponent<Enemy>().TakeDamage(
                        GetDmg() * (1 + Mathf.Floor(heatCount) * multiplierPerHeatStack *
                            multiplierPerHeatStackMultiplier),
                        damageType, transform.position))
                    {
                        Destroy(activeLasers[i]);
                        shooting = false;
                    }
                }
            }
        }
    }
}