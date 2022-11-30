using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBossPart : MonoBehaviour
{
    [SerializeField] private List<EnemyPack> enemyPacks;

    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;
    [SerializeField] private Transform spawnZone;

    private Transform enemyParent;

    void Start()
    {
        StartCoroutine("SpawnEnemyPack", Random.Range(minDelay, maxDelay));
        enemyParent = GameObject.FindGameObjectWithTag("EnemyParent").transform;
    }

    private IEnumerator SpawnEnemyPack(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnEnemies(PickRandomPack());
        EnemySpawner.FindEnemies();
        StartCoroutine("SpawnEnemyPack", Random.Range(minDelay, maxDelay));
    }

    private void SpawnEnemies(EnemyPack enemyPack)
    {
        for (int i = 0; i < enemyPack.enemies.Count; i++)
        {
            for (int j = 0; j < enemyPack.enemiesCount[i]; j++)
            {
                Instantiate(enemyPack.enemies[i], GetRandomPointOnSpawnZone(spawnZone), Quaternion.identity, enemyParent);
            }
        }        
    }

    private EnemyPack PickRandomPack()
    {
        var index = Random.Range(0, enemyPacks.Count);
        return enemyPacks[index];
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
public class EnemyPack
{
    public List<GameObject> enemies;
    public List<int> enemiesCount;
}
