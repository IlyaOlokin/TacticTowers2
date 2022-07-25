using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float shootDelay = 2.5f;
    [SerializeField] private float searchDelay = 3f;
    private bool canShoot = true;
    [SerializeField] private GameObject web;
    private Transform shootZone;
    private GameObject[] towers;

    void Start()
    {
        shootZone = transform.GetChild(1);
        towers = GameObject.FindGameObjectsWithTag("Tower");
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
            Vector3 dir = transform.position - tower.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
            if (distToTower <= shootZone.localScale.x)
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
            Instantiate(web, transform.position, transform.rotation);
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
}
