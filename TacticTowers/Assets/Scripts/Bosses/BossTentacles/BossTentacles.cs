using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossTentacles : MonoBehaviour
{
    [SerializeField] private List<Tentacle> tentaclesTips;
    [SerializeField] private float tentaclesRange;
    private float castDelay = 5f;
    void Start()
    {
        StartCoroutine("CastTentacles");
    }

    private List<Enemy> FindEnemies()
    {
        var targets = new List<Enemy>();
        
        for (var i = 0; i < EnemySpawner.enemies.Count; i++)
        {
            if (Vector2.Distance(transform.position, EnemySpawner.enemies[i].transform.position) > tentaclesRange) continue;
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
                tentacle.enemy = enemy;
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
}
