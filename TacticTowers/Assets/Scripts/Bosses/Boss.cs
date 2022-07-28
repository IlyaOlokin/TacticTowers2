using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField] private float shootDelay = 2.5f;
    [SerializeField] private float shootTime = 3f;
    private bool canShoot = true;
    [SerializeField] private GameObject web;
    private Transform shootZone;
    private Vector3 basePos;
    private GameObject[] towers;

    void Start()
    {
        shootZone = transform.GetChild(1);
        towers = GameObject.FindGameObjectsWithTag("Tower");
        basePos = GameObject.FindGameObjectWithTag("Base").transform.position;
    }

    void Update()
    {
        FindTower();
    }

    private void FindTower()
    {
        GameObject target = null;
        float distToTarget = float.MaxValue;
        foreach (var tower in towers)
        {
            if (tower == null) continue;
            var distToTower = Vector2.Distance(transform.position, tower.transform.position);
            if (distToTower <= shootZone.localScale.x * 5 && Math.Abs(tower.transform.position.x - shootZone.position.x) < 2.5)
            {
                if (target == null || distToTower < distToTarget)
                {
                    distToTarget = distToTower;
                    target = tower;
                }
            }
        }

        if (canShoot) Shoot(target);
        else Shoot(null);
    }

    public void Shoot(GameObject obj)
    {
        if (obj == null) return;
        if(canShoot)
        {
            LootAtTarget(obj);
            StartCoroutine(Shooting(obj));
            var newWeb = Instantiate(web, transform.position, transform.rotation);
            newWeb.GetComponent<Web>().endPos = obj.transform.position;
            StartCoroutine(Reload());
        }   
    }

    private void LootAtTarget(GameObject target)
    {
        Vector3 dir = transform.position - target.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle + 90);
    }

    IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    IEnumerator Shooting(GameObject target)
    {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        yield return new WaitForSeconds(shootTime);
        GetComponent<NavMeshAgent>().SetDestination(basePos);
    }
}
