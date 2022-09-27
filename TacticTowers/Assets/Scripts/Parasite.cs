using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    [NonSerialized] public GameObject tower;
    void Start()
    {
        tower.GetComponent<TowerDrag>().tower.GetComponent<Tower>().enemiesToIgnore.Add(gameObject);
    }
    
    void Update()
    {
        transform.position = tower.transform.position;
    }

    private void OnDestroy()
    {
        tower.GetComponent<TowerDrag>().tower.GetComponent<Tower>().enemiesToIgnore.Remove(gameObject);
    }
}
