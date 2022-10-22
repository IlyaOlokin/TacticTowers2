using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossWeb : Boss
{
    [SerializeField] private float shootDelay = 2.5f; 
    private float shootTimer = 0f;
    [SerializeField] private float shootDistance;
    
    [SerializeField] private GameObject web;
    [SerializeField] private GameObject gun;
    private float rotationSpeed = 12f;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = gun.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        TryToShoot();
        shootTimer += Time.deltaTime;
    }

    private void TryToShoot()
    {
        var target = FindTower();
        PointGunAtTarget(target);

        if (shootTimer >= shootDelay)
        {
            Shoot(target);
            spriteRenderer.color = Color.green; // временно
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
            
    }

    private void PointGunAtTarget(GameObject target)
    {
        if (target == null) return;

        Vector3 vectorToTarget = target.transform.position - gun.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, q, Time.deltaTime * rotationSpeed);
    }

    private void Shoot(GameObject target)
    {
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
