using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float enemyHpMultiplier = 1f;
    [SerializeField] private float enemySpeedMultiplier = 1f;
    [SerializeField] private float creditsDropChanceMultiplier = 1f;
    [SerializeField] private float enemyCountMultiplier = 1f;
    [SerializeField] private float moneyMultiplier = 1f;
    [SerializeField] private bool showIndividualBossHPBar;
    
    [Header("Waves")]
    [SerializeField] private Transform enemiesObject;

    public static List<GameObject> enemies;
    [SerializeField] private List<Wave> Waves = new List<Wave>();
    
    //[SerializeField] private List<EnemySet> enemySets = new List<EnemySet>();
    [SerializeField] private List<EnemySetGroup> enemySetGroups = new List<EnemySetGroup>();

    [SerializeField] private Transform spawnZoneRight;
    [SerializeField] private Transform spawnZoneTop;
    [SerializeField] private Transform spawnZoneLeft;
    [SerializeField] private Transform spawnZoneBot;

    [Header("UI")] 
    [SerializeField] private Text waveCount;
    [SerializeField] private BossHpBar bossHpBar;
    [SerializeField] private GameObject bossAnnouncement;

    private int currentWave = 0;
    private static bool isBossInField;
    private static bool isCurrentWaveSpecial;
    private Boss currentBoss;


    private void Start()
    {
        FindEnemies();
        isBossInField = false;
    }

    public static void FindEnemies()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        if (isBossInField)
        {
            foreach (var bossPart in GameObject.FindGameObjectsWithTag("BossPart"))
            {
                enemies.Add(bossPart);
            }
        }
    }

    private void Update()
    {
        if (isCurrentWaveSpecial)
        {
            if (!IsAnyEnemyLeft())
            {
                isBossInField = false;
                isCurrentWaveSpecial = false;
                Timer.Play();
            }
            return;
        }
        
        for (int i = 0; i < Waves.Count; i++)
        {
            if (!Waves[i].released && Timer.timer <= 0)
            {
                ReleaseWave(Waves[i], i);

                currentWave++;
                waveCount.text = $"{currentWave:00}/{Waves.Count:00}";
               
                if (i + 1 == Waves.Count)
                {
                    Timer.Stop();
                    break;
                }
                Timer.SetTimer(Waves[i + 1].seconds);
            }
        }
    }

    private void ReleaseWave(Wave wave, int i)
    {
        float waveScale = wave.waveScale;
        if (wave.isSpecial)
        {
            wave.enemySet = wave.specialEnemySet;
            waveScale = 1f;
            Timer.Stop();
            isCurrentWaveSpecial = true;
        }
        else
        {
            foreach (var enemySetGroup in enemySetGroups)
            {
                if (i >= enemySetGroup.minWave && i <= enemySetGroup.maxWave)
                {
                    wave.enemySet = enemySetGroup.enemySets[Random.Range(0, enemySetGroup.enemySets.Count)];
                }
            }
            
        }

        float weightCost = wave.moneyForWave / GetEnemyWeight(wave);

        Vector3 bossPosition = new Vector3();
        if (wave.bossTransform != null)
            bossPosition = wave.bossTransform.position;

        ReleaseWaveSide(wave.enemySet.Right, spawnZoneRight, waveScale * enemyCountMultiplier, weightCost * moneyMultiplier, bossPosition);
        ReleaseWaveSide(wave.enemySet.Top, spawnZoneTop, waveScale * enemyCountMultiplier, weightCost * moneyMultiplier, bossPosition);
        ReleaseWaveSide(wave.enemySet.Left, spawnZoneLeft, waveScale * enemyCountMultiplier, weightCost * moneyMultiplier, bossPosition);
        ReleaseWaveSide(wave.enemySet.Bot, spawnZoneBot, waveScale * enemyCountMultiplier, weightCost * moneyMultiplier, bossPosition);
        
        
        FindEnemies();

        if (wave.isSpecial)
        {
            isBossInField = FindObjectOfType<Boss>() != null;
        }

        if (SceneManager.GetActiveScene().name != "Tutorial" && isBossInField)
        {
            bossAnnouncement.SetActive(true);
            ConnectBossHpBar();
        }
        
        wave.released = true;
    }

    private void ReleaseWaveSide(List<EnemyInfo> enemyTypes, Transform spawnZone, float waveScale, float weightCost, Vector3 bossPos)
    {
        if (waveScale != 0)
            weightCost /= waveScale;
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            var enemyCount = Math.Floor(enemyTypes[i].enemyCount * waveScale);
            for (int j = 0; j < enemyCount; j++)
            {
                var newEnemy = Instantiate(enemyTypes[i].enemy, GetRandomPointOnSpawnZone(spawnZone), Quaternion.identity, enemiesObject);
                var enemyComp = newEnemy.GetComponent<Enemy>();
                enemyComp.SetMultipliers(enemyHpMultiplier, enemySpeedMultiplier, creditsDropChanceMultiplier);
                enemyComp.SetCost(enemyComp.GetWeight() * weightCost);
                if ((isCurrentWaveSpecial || showIndividualBossHPBar) && newEnemy.TryGetComponent(out Boss boss))
                {
                    enemyComp.SetHpHidden(!showIndividualBossHPBar);
                    if (bossPos.Equals(Vector3.zero)) continue;
                    boss.transform.position = bossPos;
                    currentBoss = newEnemy.GetComponent<Boss>();
                }
            }
        }
    }

    private Vector2 GetRandomPointOnSpawnZone(Transform spawnZone)
    {
        return new Vector2(
            Random.Range(spawnZone.position.x - spawnZone.localScale.x / 2f,
                spawnZone.position.x + spawnZone.localScale.x / 2f),
            Random.Range(spawnZone.position.y - spawnZone.localScale.y / 2f,
                spawnZone.position.y + spawnZone.localScale.y / 2f));
    }

    private float GetEnemyWeight(Wave wave)
    {
        float sum = CountSideWeight(wave.enemySet.Top) +
                    CountSideWeight(wave.enemySet.Bot) +
                    CountSideWeight(wave.enemySet.Left) +
                    CountSideWeight(wave.enemySet.Right);
        return sum;
    }

    private float CountSideWeight(List<EnemyInfo> enemyTypes)
    {
        float sum = 0;
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            sum += enemyTypes[i].enemy.GetComponent<Enemy>().GetWeight() * enemyTypes[i].enemyCount;
        }

        return sum;
    }

    private bool IsAnyEnemyLeft()
    {
        return enemies.Count != 0;
    }

    private void ConnectBossHpBar()
    {
        bossHpBar.gameObject.SetActive(true);
        bossHpBar.InitializeBoss(currentBoss);
    }
}

[Serializable] 
public class Wave
{
    [NonSerialized] public bool released = false;
    public float moneyForWave;
    public int seconds;
    public float waveScale;
    
    public bool isSpecial;
    public EnemySet specialEnemySet;
    public Transform bossTransform;
    
    [NonSerialized] public EnemySet enemySet;
}

[Serializable]
public struct EnemyInfo
{
    public GameObject enemy;
    public float enemyCount;
}

[Serializable]
public struct EnemySetGroup
{
    public List<EnemySet> enemySets;
    public int minWave;
    public int maxWave;
}


