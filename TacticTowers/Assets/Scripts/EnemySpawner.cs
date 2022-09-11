using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] private Transform enemiesObject;
    
    public static List<GameObject> enemies;
    [SerializeField] private List<Wave> Waves = new List<Wave>();

    [SerializeField] private Transform spawnZoneRight;
    [SerializeField] private Transform spawnZoneTop;
    [SerializeField] private Transform spawnZoneLeft;
    [SerializeField] private Transform spawnZoneBot;

    [Header("UI")] 
    [SerializeField] private Text waveCount;

    private void Start()
    {
        FindEnemies();
    }

    private static void FindEnemies()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private void Update()
    {
        for (int i = 0; i < Waves.Count; i++)
            if (!Waves[i].released && Timer.timer <= 0)
            {
                ReleaseWave(Waves[i]);

                var currentWave = i + 1;
                var totalWaves = Waves.Count;
                waveCount.text = $"{currentWave:00}/{totalWaves:00}";
               
                if (i + 1 == Waves.Count)
                {
                    Timer.Stop();
                    break;
                }
                Timer.SetTimer(Waves[i + 1].seconds - Waves[i].seconds);
            }
         
    }

    private void ReleaseWave(Wave wave)
    {
        
        float weightCost = wave.moneyForWave / GetEnemyWeight(wave);
        
        ReleaseWaveSide(wave.enemySet.Right, spawnZoneRight, weightCost);
        ReleaseWaveSide(wave.enemySet.Top, spawnZoneTop, weightCost);
        ReleaseWaveSide(wave.enemySet.Left, spawnZoneLeft, weightCost);
        ReleaseWaveSide(wave.enemySet.Bot, spawnZoneBot, weightCost);
        
        
        FindEnemies();

        wave.released = true;
    }

    private void ReleaseWaveSide(List<EnemyType> enemyTypes, Transform spawnZone, float weightCost)
    {
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            for (int j = 0; j < enemyTypes[i].enemyCount; j++)
            {
                var newEnemy = Instantiate(enemyTypes[i].enemy, GetRandomPointOnSpawnZone(spawnZone), Quaternion.identity, enemiesObject);
                newEnemy.GetComponent<Enemy>().cost = newEnemy.GetComponent<Enemy>().weight * weightCost;
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

    private float CountSideWeight(List<EnemyType> enemyTypes)
    {
        float sum = 0;
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            sum +=enemyTypes[i].enemy.GetComponent<Enemy>().weight * enemyTypes[i].enemyCount;
        }

        return sum;
    }
}

[Serializable] 
public class Wave
{
    [NonSerialized] public bool released = false;
    public float moneyForWave;

    [Header("Time in seconds")] 
    public int seconds;

    public EnemySet enemySet;
}

[Serializable]
public struct EnemyType
{
    public GameObject enemy;
    public int enemyCount;
}


