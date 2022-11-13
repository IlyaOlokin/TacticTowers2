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

    [Header("Special")] 
    /*[SerializeField] private GameObject flameStats;
    [SerializeField] private GameObject frostStats;
    [SerializeField] private GameObject laserStats;
    [SerializeField] private GameObject minigunStats;
    [SerializeField] private GameObject mortarStats;
    [SerializeField] private GameObject railStats;
    [SerializeField] private GameObject shotgunStats;
    [SerializeField] private GameObject teslaStats;*/
    [SerializeField] private List<TowerStat> specialTowerStats;


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
            case Flamethrower flamethrower:
                SetFlamethrowerValues(flamethrower);
                //flameStats.SetActive(true);
                break;
            case Frostgun frostgun:
                SetFrostgunValues(frostgun);
                //frostStats.SetActive(true);
                break;
            case Laser _:
                //laserStats.SetActive(true);
                break;
            case Minigun _:
                //minigunStats.SetActive(true);
                break;
            case Mortar _:
                //mortarStats.SetActive(true);
                break;
            case Railgun _:
                //railStats.SetActive(true);
                break;
            case Shotgun _:
                //shotgunStats.SetActive(true);
                break;
            case Tesla _:
                //teslaStats.SetActive(true);
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

    private string FloatToString(float value)
    {
        string result = value.ToString("0.0");
        if (result.Substring(result.Length - 2) == ",0") result = result.Substring(0, result.Length - 2);
        return result;
    }

    private void SetBaseValues(Tower tower)
    {
        DisableSpecialStats();
        
        //Damage
        var upgradeDamage = tower.upgrades[0];
        var greenDamage = FloatToString(tower.GetDmg() - tower.Dmg);
        baseTowerStats[0].SetData(upgradeDamage.UpgradeSprite, upgradeDamage.upgradeLabel, tower.Dmg.ToString(), greenDamage);
        //AttackSpeed
        var upgradeAttackSpeed = tower.upgrades[1];
        var greenAttackSpeed = FloatToString(tower.GetAttackSpeed() - tower.attackSpeed);
        baseTowerStats[1].SetData(upgradeAttackSpeed.UpgradeSprite, upgradeAttackSpeed.upgradeLabel, tower.attackSpeed.ToString(), greenAttackSpeed);
        //ShootAngle
        var upgradeShootAngle = tower.upgrades[2];
        var greenShootAngle = FloatToString(tower.GetShootAngle() - tower.shootAngle);
        baseTowerStats[2].SetData(upgradeShootAngle.UpgradeSprite, upgradeShootAngle.upgradeLabel, tower.shootAngle.ToString(), greenShootAngle);
        //ShootDistance
        var upgradeShootDistance = tower.upgrades[3];
        var greenShootDistance = FloatToString(tower.GetShootDistance() - tower.shootDistance);
        baseTowerStats[3].SetData(upgradeShootDistance.UpgradeSprite, upgradeShootDistance.upgradeLabel, tower.shootDistance.ToString(), greenShootDistance);
    }

    private void DisableSpecialStats()
    {
        foreach (var towerStat in specialTowerStats)
        {
            towerStat.gameObject.SetActive(false);
        }
    }

    private void SetFlamethrowerValues(Flamethrower tower)
    {
        DisableSpecialStats();
        
        //BurnDamage
        var upgradeBurnDamage = tower.upgrades[4];
        specialTowerStats[0].gameObject.SetActive(true);
        var greenBurnDamage = FloatToString(tower.burnDmg * tower.burnDmgMultiplier - tower.burnDmg);
        specialTowerStats[0].SetData(upgradeBurnDamage.UpgradeSprite, upgradeBurnDamage.upgradeLabel, tower.burnDmg.ToString(), greenBurnDamage);
        
        //BurnTime
        var upgradeBurnTime = tower.upgrades[5];
        specialTowerStats[1].gameObject.SetActive(true);
        var greenBurnTime = FloatToString(tower.burnTime * tower.burnTimeMultiplier - tower.burnTime);
        specialTowerStats[1].SetData(upgradeBurnTime.UpgradeSprite, upgradeBurnTime.upgradeLabel, tower.burnTime.ToString(), greenBurnTime);
    }
    
    private void SetFrostgunValues(Frostgun tower)
    {
        DisableSpecialStats();
        
        //TimeToFreeze
        var upgradeBurnDamage = tower.upgrades[4];
        specialTowerStats[0].gameObject.SetActive(true);
        var totalTimeToFreeze = tower.freezeStacksNeeded /
                                (tower.freezeStacksPerHit * tower.freezeStacksPerHitMultiplier * tower.GetAttackSpeed());
        var whiteTimeToFreeze = tower.freezeStacksNeeded /
                                (tower.freezeStacksPerHit * tower.attackSpeed);
        var greenTimeToFreeze = FloatToString(totalTimeToFreeze - whiteTimeToFreeze);
        specialTowerStats[0].SetData(upgradeBurnDamage.UpgradeSprite, upgradeBurnDamage.upgradeLabel, FloatToString(whiteTimeToFreeze), greenTimeToFreeze);
        
        //FreezeTime
        var upgradeBurnTime = tower.upgrades[5];
        specialTowerStats[1].gameObject.SetActive(true);
        var greenBurnTime = FloatToString(tower.freezeTime * tower.freezeTimeMultiplier - tower.freezeTime);
        specialTowerStats[1].SetData(upgradeBurnTime.UpgradeSprite, upgradeBurnTime.upgradeLabel, tower.freezeTime.ToString(), greenBurnTime);
    }
}
