using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossParasite : Boss
{
    [Header("BossParasite")]
    [SerializeField] private float shootDelay; 
    private float shootTimer = 0f;
    
    [Header("Parasite")]
    [SerializeField] private GameObject parasite;
    [SerializeField] private GameObject parasiteLine;
    [SerializeField] private float attackSpeedMultiplier;
    private int parasiteCount = 2;

    private void Update()
    {
        base.Update();
        TryToShoot();
        UpdateHp();
        shootTimer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.A))
            Shoot(GameObject.FindGameObjectsWithTag("Tower").ToList());
    }
    
    private void TryToShoot()
    {
        if (shootTimer >= shootDelay)
        {
            var towers = GameObject.FindGameObjectsWithTag("Tower")
                .Where(t => !t.GetComponent<TowerDrag>().tower.GetComponent<Tower>().HasParasite()).ToArray();
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
            if (targets.Count == 0) return;
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
            var newParasite = Instantiate(parasite, tower.transform.position, Quaternion.identity, tower.transform);
            newParasite.GetComponent<Parasite>().tower = tower;
            newParasite.GetComponent<Parasite>().attackSpeedMultiplier = attackSpeedMultiplier;
            EnemySpawner.enemies.Add(newParasite);
            
            var newParasiteLine = Instantiate(parasiteLine, tower.transform.position, Quaternion.identity);
            var lineRenderer = newParasiteLine.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, tower.transform.position);
        }

        shootTimer = 0;
    }
}
