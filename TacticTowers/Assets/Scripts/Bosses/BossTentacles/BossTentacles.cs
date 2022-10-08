using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTentacles : MonoBehaviour
{
    [SerializeField] private List<GameObject> tentaclesTips;
    [SerializeField] private float tentaclesRange;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private List<Enemy> FindEnemies()
    {
        var targets = new List<Enemy>();
        
        for (var i = 0; i < EnemySpawner.enemies.Count; i++)
        {
            if (Vector2.Distance(transform.position, EnemySpawner.enemies[i].transform.position) < tentaclesRange) continue;
            
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
}
