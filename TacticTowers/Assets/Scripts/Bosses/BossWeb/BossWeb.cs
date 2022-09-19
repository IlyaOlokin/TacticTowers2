using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossWeb : MonoBehaviour
{
    [SerializeField] private float shootDelay = 2.5f; 
    private float shootTimer = 0f;
    [SerializeField] private float shootDistance;
    
    [SerializeField] private GameObject web;

    void Update()
    {
        TryToShoot();
        shootTimer += Time.deltaTime;
    }

    private void TryToShoot()
    {
        if (shootTimer >= shootDelay)
            Shoot();
    }
    
    private void Shoot()
    {
        var target = FindTower();
        if (target == null) return;

        shootTimer = 0;
        
        var newWeb = Instantiate(web, transform.position, transform.rotation);
        newWeb.GetComponent<Web>().endPos = target.transform.position;
    }

    private GameObject FindTower()
    {
        var towers = GameObject.FindGameObjectsWithTag("TowerInstance");
        var minDist = float.MaxValue;
        GameObject targetTower = null;
        foreach (var tower in towers)
        {
            if (tower == null) continue;
            if (!tower.GetComponent<Tower>().CanShoot()) continue;
            
            var distToTower = Vector2.Distance(transform.position, tower.transform.position);
            
            if (distToTower <= shootDistance && distToTower < minDist)
            {
                targetTower = tower;
                minDist = distToTower;
            }
        }

        return targetTower;
    }
}
