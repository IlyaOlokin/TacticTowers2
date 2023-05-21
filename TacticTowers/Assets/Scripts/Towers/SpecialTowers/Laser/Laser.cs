using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Laser : Tower
{
    [SerializeField] private GameObject laserBim;
    [SerializeField] private GameObject shootPoint;

    private float heatCount = 0;
    private float coolTimer;
    private List<GameObject> currentEnemies = new List<GameObject>() {null, null};
    private List<GameObject> extraCurrentEnemies = new List<GameObject>() {null, null, null, null};
    private List<GameObject> activeLasers = new List<GameObject>() {null, null};
    private List<List<GameObject>> extraActiveLasers = new List<List<GameObject>>() {new List<GameObject>() {null, null}, new List<GameObject>() {null, null}};
    private List<bool> laserSound = new List<bool>() {false, false};

    [SerializeField] public int maxHeat;
    [SerializeField] public float maxHeatMultiplier;
    [SerializeField] public float multiplierPerHeatStack;
    [SerializeField] public float multiplierPerHeatStackMultiplier;
    [SerializeField] public float coolDelay;
    [SerializeField] public float coolDelayMultiplier;
    [SerializeField] private ContactFilter2D contactFilter;

    [NonSerialized] public bool shooting;
    private DamageType damageType = DamageType.Fire;

    [NonSerialized] public bool hasSecondBeamUpgrade;
    
    [NonSerialized] public bool hasSlowUpgrade;
    [Header("Slow Upgrade")] 
    [SerializeField] private float slowAmount;
    [SerializeField] private float slowDuration;

    [NonSerialized] public bool hasBranchingBeamUpgrade;
    [Header("Branching Beam Upgrade")]
    [SerializeField] private int extraLaserCount = 2;
    [SerializeField] private float extraLaserDamageMultiplier;

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
        if (hasSecondBeamUpgrade && isMaxHeated()) 
            LaserShoot(FindTarget(new List<GameObject>(){enemy}.Union(extraCurrentEnemies)), 1);
        
        if (enemy == null) return;
        LootAtTarget(MiddleEnemyPoint());
        DealDamage();
    }

    private void LaserShoot(GameObject target, int i)
    {
        if (target == null)
        {
            DeactivateLaser(i);
            DeactivateExtraLasers(i);
            return;
        }

        if (heatCount < maxHeat * maxHeatMultiplier) heatCount += Time.deltaTime;
        if (activeLasers[i] != null) activeLasers[i].GetComponent<LaserBeam>().IncreaseWidth(heatCount);
        
        if (target != currentEnemies[i])
        {
            DeactivateExtraLasers(i);
            Destroy(activeLasers[i]);
            DeactivateLaserSound(i);
            activeLasers[i] = Instantiate(laserBim, transform.position, towerCanon.transform.rotation);
            var laserBeamComp = activeLasers[i].GetComponent<LaserBeam>();
            laserBeamComp.target = target;
            laserBeamComp.origin = gameObject;
            currentEnemies[i] = target;
            ActivateLaserSound(i);
        }
        
        if (hasBranchingBeamUpgrade) HandleExtraLasers(target, i);
    }

    private void HandleExtraLasers(GameObject target, int i)
    {
        for (int j = 0; j < extraLaserCount; j++)
        {
            var enemyToIgnore = currentEnemies.Union(extraCurrentEnemies).ToList();
            enemyToIgnore.Remove(extraCurrentEnemies[i * 2 + j]);
            var closetEnemy = FindClosetEnemy(target.transform.position, enemyToIgnore, 1.5f);

            if (CheckWallCollision(transform.position, target.transform.position, GetShootDistance(), false) is
                null)
            {
                ExtraLaserShoot(target, closetEnemy, i, j);
            }
        }
    }

    private void ExtraLaserShoot(GameObject origin, GameObject target, int parentLaserIndex, int extraLaserIndex)
    {
        if (target == null || origin == null)
        {
            DeactivateExtraLasers(parentLaserIndex);
            return;
        }
        
        if (extraActiveLasers[parentLaserIndex][extraLaserIndex] != null) extraActiveLasers[parentLaserIndex][extraLaserIndex].GetComponent<LaserBeam>().IncreaseWidth(heatCount);
        
        if (target != extraCurrentEnemies[parentLaserIndex * 2 + extraLaserIndex])
        {
            Destroy(extraActiveLasers[parentLaserIndex][extraLaserIndex]);
            extraActiveLasers[parentLaserIndex][extraLaserIndex] = Instantiate(laserBim, transform.position, towerCanon.transform.rotation);
            var laserBeamComp = extraActiveLasers[parentLaserIndex][extraLaserIndex].GetComponent<LaserBeam>();

            laserBeamComp.target = target;
            laserBeamComp.origin = origin;
            laserBeamComp.scaleMultiplier = 0.5f;
            extraCurrentEnemies[parentLaserIndex * 2 + extraLaserIndex] = target;
        }
    }

    private void DealDamage()
    {
        bool needToDealDamage = false;
        foreach (var enemy in currentEnemies)
        {
            if (enemy != null)
            {
                needToDealDamage = true;
                break;
            }
        }
        if (!needToDealDamage) return;
        
        if (shootDelayTimer <= 0)
        {
            shootDelayTimer = 1f / GetAttackSpeed();
            coolTimer = coolDelay * coolDelayMultiplier;

            DealDamageToGroup(currentEnemies, activeLasers);
            var extraLasers = extraActiveLasers[0].Union(extraActiveLasers[1]).ToList();
            DealDamageToGroup(extraCurrentEnemies, extraLasers,  extraLaserDamageMultiplier);
        }
    }

    private void DealDamageToGroup(List<GameObject> enemies, List<GameObject> lasers, float damageMultiplier = 1f)
    {
        for (var i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null) continue;
            if (CheckWallCollision(transform.position, enemies[i].transform.position, GetShootDistance(), false) is
                null)
            {
                if (enemies[i].GetComponent<Enemy>().TakeDamage(
                    GetDmg() * (1 + Mathf.Floor(heatCount) * multiplierPerHeatStack *
                        multiplierPerHeatStackMultiplier) * damageMultiplier,
                    damageType, transform.position))
                {
                    Destroy(lasers[i]);
                    shooting = false;
                }
                if (hasSlowUpgrade) enemies[i].GetComponent<Enemy>().TakeSlow(slowAmount, slowDuration);
            }
        }
    }
    
    private void DeactivateLaser(int i)
    {
        Destroy(activeLasers[i]);
        DeactivateLaserSound(i);
        currentEnemies[i] = null;
    }
    
    private void DeactivateExtraLasers(int parentLaserIndex)
    {
        if (hasBranchingBeamUpgrade)
            for (int j = 0; j < extraLaserCount; j++)
            {
                Destroy(extraActiveLasers[parentLaserIndex][j]);
                extraCurrentEnemies[parentLaserIndex * 2 + j] = null;
            }
    }

    private void ActivateLaserSound(int i)
    {
        laserSound[i] = true;
        HandleLaserSound();
    }
    
    private void DeactivateLaserSound(int i)
    {
        laserSound[i] = false;
        HandleLaserSound();
    }

    private void HandleLaserSound()
    {
        if (laserSound.Contains(true))
        {
            if (!audioSrc.isPlaying) audioSrc.Play();
        }
        else audioSrc.Stop();
    }
    
    private bool isMaxHeated()
    {
        return heatCount >= maxHeat * maxHeatMultiplier;
    }

    private Vector3 MiddleEnemyPoint()
    {
        Vector3 posSum = Vector3.zero;
        int enemiesCount = 0;
        foreach (var enemy in currentEnemies)
            if (enemy != null)
            {
                posSum += enemy.transform.position;
                enemiesCount++;
            }
        
        return posSum / enemiesCount;
    }
}