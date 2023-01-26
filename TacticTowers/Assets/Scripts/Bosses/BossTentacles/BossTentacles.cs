using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossTentacles : Boss
{
    [SerializeField] private List<Tentacle> tentaclesTips;
    public float tentaclesRange;
    [SerializeField] private float regenForTentacle;
    [SerializeField] private GameObject tentacleParent;
    //private Enemy enemyComp;
    //private float maxHp;
    private float tentacleConnected;
    private float castDelay = 5f;
    void Start()
    {
        StartCoroutine("CastTentacles");
        enemyComp = GetComponent<Enemy>();
        maxHp = enemyComp.hp;
    }

    private void Update()
    {
        if (isDead) return;
        Regenerate();
        UpdateHp();
    }

    private void Regenerate()
    {
        if (enemyComp.hp < maxHp)
            enemyComp.hp += regenForTentacle * tentacleConnected * Time.deltaTime;
        else
            enemyComp.hp = maxHp;
    }

    private List<Enemy> FindEnemies()
    {
        var targets = new List<Enemy>();
        
        for (var i = 0; i < EnemySpawner.enemies.Count; i++)
        {
            if (Vector2.Distance(transform.position, EnemySpawner.enemies[i].transform.position) > tentaclesRange) continue;
            if (EnemySpawner.enemies[i].GetComponent<Enemy>().hasTentacle) continue;
            if (EnemySpawner.enemies[i] == gameObject) continue;
            
            var probabilityOfSelection = (tentaclesTips.Count - targets.Count) / (float) (EnemySpawner.enemies.Count - i);
            probabilityOfSelection *= 100;
            int chance = Random.Range(0, 100);
            if (chance < probabilityOfSelection)
            {
                targets.Add(EnemySpawner.enemies[i].GetComponent<Enemy>());
            }
        }

        return targets;
    }

    private void ShootTentaclesToEnemies(List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            foreach (var tentacle in tentaclesTips)
            {
                if (tentacle.enemy != null) continue;
                tentacle.SetEnemyAsTarget(enemy);
                ConnectTentacle();
                break;
            }
        }
    }

    IEnumerator CastTentacles()
    {
        yield return new WaitForSeconds(castDelay);
        ShootTentaclesToEnemies(FindEnemies());
        StartCoroutine("CastTentacles");
    }

    public void ConnectTentacle()
    {
        tentacleConnected++;
    }

    public void DisconnectTentacle()
    {
        tentacleConnected--;
    }

    protected override void BossDeath()
    {
        isDead = true;
        Destroy(tentacleParent);
    }
}
