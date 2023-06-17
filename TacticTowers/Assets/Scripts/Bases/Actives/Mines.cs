using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : BaseActive
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject Mine;
    [SerializeField] private int countMines;
    
    private void Start() => audioSrc = GetComponent<AudioSource>();
    
    public override void ExecuteActiveAbility()
    {
        spawner.GetComponent<MinesSpawner>().audioSrc = audioSrc;
        spawner.GetComponent<MinesSpawner>().Mine = Mine;
        spawner.GetComponent<MinesSpawner>().countMines = countMines;
        spawner.SetActive(true);
        spawner.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawner.GetComponent<MinesSpawner>().baseActive = this;
        isAiming = true;
    }

    public override void CancelAiming()
    {
        isAiming = false;
        spawner.SetActive(false);
    }
}
