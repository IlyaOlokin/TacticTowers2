using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeBig : Enemy
{
    [Header("Slime Big")] 
    [SerializeField] private GameObject slimeMedium;
    [SerializeField] private int slimeAmount;
    
    private void OnDestroy()
    {
        if(!gameObject.scene.isLoaded) 
            return;
        
        var enemyParent = GameObject.FindGameObjectWithTag("EnemyParent").transform;
        for (var i = 0; i < slimeAmount; i++)
            Instantiate(slimeMedium, transform.position, Quaternion.identity, enemyParent);
        
        EnemySpawner.FindEnemies();
    }
}
