using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStatWindow : MonoBehaviour
{
    [Header("Regular")] [SerializeField] private GameObject dmgStat;
    [SerializeField] private GameObject firerateStat;
    [SerializeField] private GameObject ballisticStat;
    [SerializeField] private GameObject angleStat;

    [Header("Special")] [SerializeField] private GameObject flameStats;
    [SerializeField] private GameObject frostStats;
    [SerializeField] private GameObject laserStats;
    [SerializeField] private GameObject minigunStats;
    [SerializeField] private GameObject mortarStats;
    [SerializeField] private GameObject railStats;
    [SerializeField] private GameObject shotgunStats;
    [SerializeField] private GameObject teslaStats;

    [Header("idk")] [SerializeField] private Image upgradingTower;

    [SerializeField] private Tower tower;

    public void OnButtonClose()
    {
        gameObject.SetActive(false);
        TimeManager.Resume();
    }

    public void SetTower(Tower tower)
    {
        this.tower = tower;
    }
    
    private void OnEnable()
    {
        TimeManager.Pause();
        
        switch (tower)
        {
            case DefaultTower _:
                break;
            case Flamethrower _:
                flameStats.SetActive(true);
                break;
            case Frostgun _:
                frostStats.SetActive(true);
                break;
            case Laser _:
                laserStats.SetActive(true);
                break;
            case Minigun _:
                minigunStats.SetActive(true);
                break;
            case Mortar _:
                mortarStats.SetActive(true);
                break;
            case Railgun _:
                railStats.SetActive(true);
                break;
            case Shotgun _:
                shotgunStats.SetActive(true);
                break;
            case Tesla _:
                teslaStats.SetActive(true);
                break;
        }

        ShowUpgradingTower();
    }

    private void ShowUpgradingTower()
    {
        upgradingTower.sprite = tower.towerSprite;
        upgradingTower.transform.rotation = Quaternion.Euler(0,0,tower.shootDirection - 90);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
