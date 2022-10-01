using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    [NonSerialized] public GameObject tower;
    [NonSerialized] public float attackSpeedMultiplier;
    private Vector3 targetScale;
    void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = new Vector3(0,0,0);

        AttachToTower();
    }

    private void AttachToTower()
    {
        var towerComp = tower.GetComponent<TowerDrag>().tower.GetComponent<Tower>();
        towerComp.enemiesToIgnore.Add(gameObject);
        towerComp.GetParasite(attackSpeedMultiplier);
    }

    void Update()
    {
        transform.position = tower.transform.position;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (tower == null) return;
        //if (tower.GetComponent<TowerDrag>() == null) return;
        //if (tower.GetComponent<TowerDrag>().tower.GetComponent<Tower>() == null) return;


        var towerComp = tower.GetComponent<TowerDrag>().tower.GetComponent<Tower>();
        towerComp.enemiesToIgnore.Remove(gameObject);
        towerComp.LostParasite();
    }
}
