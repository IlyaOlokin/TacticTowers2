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
                waveCount.text = (i + 1) + "/" + Waves.Count;
                Timer.SetTimer(Waves[i + 1].seconds - Waves[i].seconds);
            }
         
    }

    private void ReleaseWave(Wave wave)
    {
        
        float weightCost = wave.moneyForWave / GetEnemyWeight(wave);
        
        ReleaseWaveSide(wave.enemiesRight, wave.enemiesCountRight, spawnZoneRight, weightCost);
        ReleaseWaveSide(wave.enemiesTop, wave.enemiesCountTop, spawnZoneTop, weightCost);
        ReleaseWaveSide(wave.enemiesLeft, wave.enemiesCountLeft, spawnZoneLeft, weightCost);
        ReleaseWaveSide(wave.enemiesBot, wave.enemiesCountBot, spawnZoneBot, weightCost);
        
        
        FindEnemies();

        wave.released = true;
    }

    private void ReleaseWaveSide(List<GameObject> enemies, List<int> enemiesCount, Transform spawnZone, float weightCost)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemiesCount[i]; j++)
            {
                var newEnemy = Instantiate(enemies[i], GetRandomPointOnSpawnZone(spawnZone), Quaternion.identity, enemiesObject);
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
        float sum = CountSideWeight(wave.enemiesTop, wave.enemiesCountTop) +
                    CountSideWeight(wave.enemiesBot, wave.enemiesCountBot) +
                    CountSideWeight(wave.enemiesLeft, wave.enemiesCountLeft) +
                    CountSideWeight(wave.enemiesRight, wave.enemiesCountRight);
        return sum;
    }

    private float CountSideWeight(List<GameObject> enemies, List<int> enemyCount)
    {
        float sum = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            sum +=enemies[i].GetComponent<Enemy>().weight * enemyCount[i];
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
    [Header("Right Side")]
    public List<GameObject> enemiesRight = new List<GameObject>();
    public List<int> enemiesCountRight = new List<int>();
    [Header("Top Side")]
    public List<GameObject> enemiesTop = new List<GameObject>();
    public List<int> enemiesCountTop = new List<int>();
    [Header("Left Side")]
    public List<GameObject> enemiesLeft = new List<GameObject>();
    public List<int> enemiesCountLeft = new List<int>();
    [Header("Bot Side")]
    public List<GameObject> enemiesBot = new List<GameObject>();
    public List<int> enemiesCountBot = new List<int>();

}


