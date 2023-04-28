using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossWomb : Boss
{
    [Header("BossWomb")]
    [SerializeField] private List<EnemyPack> enemyPacks;
    [SerializeField] private Transform spawnZone;
    [SerializeField] private float spawningDelay;
    [SerializeField] private float eachEnemySpawnDelay;

    private List<Vector3> path;
    [SerializeField] private float nearThreshold = 0.001f;
    private bool isClockwise;

    private void Awake()
    {
        EnemyMover = new EnemyMoverAir(initialSpeed, GameObject.FindGameObjectWithTag("Base").transform.position);
    }
    
    private void Start()
    {
        base.Start();
        path = new List<Vector3>
        {
            new Vector3(-7.0f, 0.5f, 0f),
            new Vector3(-4.5f, 2.5f, 0f),
            new Vector3(-2.0f, 4f, 0f),
            new Vector3(2.0f, 4f, 0f),
            new Vector3(5.0f, 2f, 0f),
            new Vector3(7.0f, -0.5f, 0f),
            new Vector3(5.0f, -2f, 0f),
            new Vector3(2.0f, -4f, 0f),
            new Vector3(-2.0f, -4f, 0f),
            new Vector3(-5.0f, -2f, 0f),
        };
        isClockwise = Random.Range(0, 2) == 0;
        EnemyMover.ChangeTarget(isClockwise ? path[0] : path[path.Count - 1]);
        StartCoroutine(nameof(SpawnEnemyPack));
    }

    private void Update()
    {
        base.Update();

        for (var i = 0; i < path.Count; i++)
        {
            if (Math.Abs(transform.position.x - path[i].x) < nearThreshold
                && Math.Abs(transform.position.y - path[i].y) < nearThreshold)
            {
                EnemyMover.ChangeTarget(isClockwise ? path[(i + 1) % path.Count] : path[Math.Abs((i - 1) % path.Count)]);

                nearThreshold = 0.001f;
                return;
            }
        }

        nearThreshold = nearThreshold > 2
            ? 0.001f
            : nearThreshold * 2.0f;
    }
    
    private IEnumerator SpawnEnemyPack()
    {
        yield return new WaitForSeconds(spawningDelay);
        animator.enabled = false;
        EnemyMover.StopMovement();
        LookAtBase();
        
        StartCoroutine(nameof(SpawnEnemies), PickRandomPack());
    }

    private void SpawnEnemies(EnemyPack enemyPack)
    {
        var enemyParent = GameObject.FindGameObjectWithTag("EnemyParent").transform;
        for (var i = 0; i < enemyPack.enemies.Count; i++)
        {
            for (var j = 0; j < enemyPack.enemiesCount[i]; j++)
            {
                Instantiate(enemyPack.enemies[i], GetRandomPointOnSpawnZone(spawnZone), transform.rotation, enemyParent);
            }
        }    
        EnemySpawner.FindEnemies();
        animator.enabled = true;
        EnemyMover.StartMovement();
        StartCoroutine(nameof(SpawnEnemyPack));
    }
    
    private IEnumerator SpawnEnemies(int enemiesLeft)
    {
        if (enemiesLeft == 0)
        {
            animator.enabled = true;
            EnemyMover.StartMovement();
            StartCoroutine(nameof(SpawnEnemyPack));
            yield break;
        }
        yield return new WaitForSeconds(eachEnemySpawnDelay);
        
        var enemyParent = GameObject.FindGameObjectWithTag("EnemyParent").transform;
        //Instantiate(spawningEnemy, spawnPoint.transform.position, transform.rotation, enemyParent);
        
        EnemySpawner.FindEnemies();
        
        //StartCoroutine(nameof(SpawnEnemies), enemiesLeft - 1);
    }

    private void LookAtBase()
    {
        var target = GameObject.FindGameObjectWithTag("Base").transform;
        var direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }
    
    private EnemyPack PickRandomPack() => enemyPacks[Random.Range(0, enemyPacks.Count)];
    
    private Vector2 GetRandomPointOnSpawnZone(Transform spawnZone)
    {
        return new Vector2(
            Random.Range(spawnZone.position.x - spawnZone.localScale.x / 2f * transform.localScale.x,
                spawnZone.position.x + spawnZone.localScale.x / 2f * transform.localScale.x),
            Random.Range(spawnZone.position.y - spawnZone.localScale.y / 2f * transform.localScale.y,
                spawnZone.position.y + spawnZone.localScale.y / 2f * transform.localScale.y));
    }
}