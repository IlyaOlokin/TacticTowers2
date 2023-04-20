using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarmer : Enemy
{
    [Header("Swarmer")] 
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject spawningEnemy;
    [SerializeField] private int spawningAmount;
    [SerializeField] private float spawningDelay;
    [SerializeField] private float eachEnemySpawnDelay;

    private List<Vector3> path;
    private float nearThreshold = 0.001f;
    private bool hasArrivedOnPath;
    
    private void Start()
    {
        base.Start();
        path = new List<Vector3>
        {
            new Vector3(-7.0f, 0.5f, 0f),
            new Vector3(-5.0f, 2f, 0f),
            new Vector3(-2.0f, 4f, 0f),
            new Vector3(2.0f, 4f, 0f),
            new Vector3(5.0f, 2f, 0f),
            new Vector3(7.0f, -0.5f, 0f),
            new Vector3(5.0f, -2f, 0f),
            new Vector3(2.0f, -4f, 0f),
            new Vector3(-2.0f, -4f, 0f),
            new Vector3(-5.0f, -2f, 0f),
        };
        EnemyMover.ChangeTarget(path[0]);
        StartCoroutine(nameof(SpawnEnemies));
    }

    private void Update()
    {
        base.Update();

        for (var i = 0; i < path.Count; i++)
        {
            if (Math.Abs(transform.position.x - path[i].x) < nearThreshold
                && Math.Abs(transform.position.y - path[i].y) < nearThreshold)
            {
                EnemyMover.ChangeTarget(path[(i + 1) % path.Count]);
                nearThreshold = 0.001f;
            }
        }

        nearThreshold = nearThreshold > 2
            ? 0.001f
            : nearThreshold * 2.0f;
    }
    
    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawningDelay);
        
        EnemyMover.StopMovement();

        StartCoroutine(nameof(SpawnEnemy), spawningAmount);
    }

    private IEnumerator SpawnEnemy(int enemiesLeft)
    {
        if (enemiesLeft == 0)
        {
            EnemyMover.StartMovement();
            StartCoroutine(nameof(SpawnEnemies));
            yield break;
        }
        yield return new WaitForSeconds(eachEnemySpawnDelay);
        
        var enemyParent = GameObject.FindGameObjectWithTag("EnemyParent").transform;
        Instantiate(spawningEnemy, spawnPoint.transform.position, Quaternion.identity, enemyParent);
        
        EnemySpawner.FindEnemies();
        
        StartCoroutine(nameof(SpawnEnemy), enemiesLeft - 1);
    }
}