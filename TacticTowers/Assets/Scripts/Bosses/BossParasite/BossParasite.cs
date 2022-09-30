using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossParasite : MonoBehaviour
{
    [SerializeField] private float shootDelay; 
    private float shootTimer = 0f;
    private int parasiteCount = 2;
    
    [SerializeField] private GameObject parasite;

    void Update()
    {
        TryToShoot();
        shootTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A))
        {
            Shoot(GameObject.FindGameObjectsWithTag("Tower").ToList());
        }
    }
    
    private void TryToShoot()
    {
        if (shootTimer >= shootDelay)
        {
            var towers = GameObject.FindGameObjectsWithTag("Tower");
            var targets = new List<GameObject>();
            for (var i = 0; i < towers.Length; i++)
            {
                var probabilityOfSelection = (parasiteCount - targets.Count) / (float) (towers.Length - i);
                probabilityOfSelection *= 100;
                int chance = Random.Range(0, 100);
                if (chance < probabilityOfSelection)
                {
                    targets.Add(towers[i]);
                }
            }
            
            Shoot(targets);
            //spriteRenderer.color = Color.green; // временно
        }
        else
        {
            //spriteRenderer.color = Color.red;
        }
            
    }

    private void Shoot(List<GameObject> towers)
    {
        foreach (var tower in towers)
        {
            var newParasite = Instantiate(parasite, tower.transform.position, Quaternion.identity);
            newParasite.GetComponent<Parasite>().tower = tower;
            EnemySpawner.enemies.Add(newParasite);
        }

        shootTimer = 0;
    }
}
