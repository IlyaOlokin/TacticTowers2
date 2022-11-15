using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : BaseActive
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject Mine;
    [SerializeField] private int countMines;

    public override void ExecuteActiveAbility()
    {
        spawner.GetComponent<MinesSpawner>().Mine = Mine;
        spawner.GetComponent<MinesSpawner>().countMines = countMines;
        spawner.SetActive(true);
    }
    

}
