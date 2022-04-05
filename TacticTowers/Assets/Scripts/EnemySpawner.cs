using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform enemiesObject;
    
    public static List<GameObject> enemies;
    [SerializeField] private List<Wave> Waves = new List<Wave>();

    [SerializeField] private Transform spawnZoneRight;
    [SerializeField] private Transform spawnZoneTop;
    [SerializeField] private Transform spawnZoneLeft;
    [SerializeField] private Transform spawnZoneBot;
    
    
    
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
            if (!Waves[i].released && Timer.timer >= Waves[i].seconds)
                ReleaseWave(Waves[i]);
         
    }

    private void ReleaseWave(Wave wave)
    {
        ReleaseWaveSide(wave.enemiesRight, wave.enemiesCountRight, spawnZoneRight);
        ReleaseWaveSide(wave.enemiesTop, wave.enemiesCountTop, spawnZoneTop);
        ReleaseWaveSide(wave.enemiesLeft, wave.enemiesCountLeft, spawnZoneLeft);
        ReleaseWaveSide(wave.enemiesBot, wave.enemiesCountBot, spawnZoneBot);
        FindEnemies();

        wave.released = true;
    }

    private void ReleaseWaveSide(List<GameObject> enemies, List<int> enemiesCount, Transform spawnZone)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemiesCount[i]; j++)
            {
                Instantiate(enemies[i], GetRandomPointOnSpawnZone(spawnZone), Quaternion.identity, enemiesObject);
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
}

[Serializable] 
public class Wave
{
    [NonSerialized] public bool released = false;
    
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

