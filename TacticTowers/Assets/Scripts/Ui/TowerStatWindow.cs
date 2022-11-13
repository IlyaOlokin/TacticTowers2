using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStatWindow : MonoBehaviour
{
    [Header("Regular")]
    /*[SerializeField] private GameObject dmgStat;
    [SerializeField] private GameObject firerateStat;
    [SerializeField] private GameObject ballisticStat;
    [SerializeField] private GameObject angleStat;*/
    [SerializeField] private List<TowerStat> baseTowerStats;

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
        
        SetBaseValues(tower);
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
    
    private void OnEnable()
    {
        TimeManager.Pause();
    }

    private void ShowUpgradingTower()
    {
        upgradingTower.sprite = tower.towerSprite;
        upgradingTower.transform.rotation = Quaternion.Euler(0,0,tower.shootDirection - 90);
    }

    private string FloatToString(float multipliedValue, float baseValue)
    {
        string result = (multipliedValue - baseValue).ToString("0.0");
        if (result.Substring(result.Length - 2) == ",0") result = result.Substring(0, result.Length - 2);
        return result;
    }

    private void SetBaseValues(Tower tower)
    {
        //Damage
        var upgradeDamage = tower.upgrades[0];
        var greenDamage = FloatToString(tower.GetDmg(), tower.Dmg);
        baseTowerStats[0].SetData(upgradeDamage.UpgradeSprite, upgradeDamage.upgradeLabel, tower.Dmg.ToString(), greenDamage);
        //AttackSpeed
        var upgradeAttackSpeed = tower.upgrades[1];
        var greenAttackSpeed = FloatToString(tower.GetAttackSpeed(), tower.attackSpeed);
        baseTowerStats[1].SetData(upgradeAttackSpeed.UpgradeSprite, upgradeAttackSpeed.upgradeLabel, tower.attackSpeed.ToString(), greenAttackSpeed);
        //ShootAngle
        var upgradeShootAngle = tower.upgrades[2];
        var greenShootAngle = (tower.GetShootAngle() - tower.shootAngle).ToString("R");
        baseTowerStats[2].SetData(upgradeShootAngle.UpgradeSprite, upgradeShootAngle.upgradeLabel, tower.shootAngle.ToString(), greenShootAngle);
        //ShootDistance
        var upgradeShootDistance = tower.upgrades[3];
        var greenShootDistance = (tower.GetShootDistance() - tower.shootDistance).ToString("R");
        baseTowerStats[3].SetData(upgradeShootDistance.UpgradeSprite, upgradeShootDistance.upgradeLabel, tower.shootDistance.ToString(), greenShootDistance);
    }
}
