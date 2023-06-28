using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    public static float ParasitesLifeTime;
    
    [NonSerialized] public GameObject tower;
    [NonSerialized] public float attackSpeedMultiplier;
    
    void Start()
    {
        AttachToTower();
    }

    private void Update()
    {
        ParasitesLifeTime += Time.unscaledDeltaTime;
    }

    private void AttachToTower()
    {
        var towerComp = tower.GetComponent<TowerDrag>().tower;
        //var towerComp = tower.GetComponent<Tower>();
        towerComp.enemiesToIgnore.Add(gameObject);
        towerComp.GetParasite(attackSpeedMultiplier);
    }

    private void OnDestroy()
    {
        if (tower == null) 
            return;
        
        var towerComp = tower.GetComponent<TowerDrag>().tower;
        //var towerComp = tower.GetComponent<Tower>();
        towerComp.enemiesToIgnore.Remove(gameObject);
        towerComp.LostParasite();
    }
}
