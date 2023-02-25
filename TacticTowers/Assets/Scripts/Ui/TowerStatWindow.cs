using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TowerStatWindow : MonoBehaviour
{
    [Header("Regular")]
    [SerializeField] private List<TowerStat> baseTowerStats;

    [Header("Special")]
    [SerializeField] private List<TowerStat> specialTowerStats;


    [Header("idk")] [SerializeField] private Image upgradingTower;

    [SerializeField] private Tower tower;
    [SerializeField] private AudioMixer audioMixer;


    public void OnButtonClose()
    {
        gameObject.SetActive(false);
        TimeManager.Resume(audioMixer);
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
                break;
            case Frostgun frostgun:
                SetFrostgunValues(frostgun);
                break;
            case Laser laser:
                SetLaserValues(laser);
                break;
            case Minigun minigun:
                SetMinigunValues(minigun);
                break;
            case Mortar mortar:
                SetMortarValues(mortar);
                break;
            case Railgun railgun:
                SetRailgunValues(railgun);
                break;
            case Shotgun shotgun:
                SetShotgunValues(shotgun);
                break;
            case Tesla tesla:
                SetTeslaValues(tesla);
                break;
        }

        ShowUpgradingTower();
    }
    
    private void OnEnable()
    {
        TimeManager.Pause(audioMixer);
    }

    private void ShowUpgradingTower()
    {
        upgradingTower.sprite = tower.towerSprites[0];
        upgradingTower.transform.rotation = Quaternion.Euler(0,0,tower.shootDirection - 90);
    }

    private string FloatToString(float value, string format, string floatPart)
    {
        string result = value.ToString(format);
        if (result.Substring(result.Length - floatPart.Length) == floatPart) result = result.Substring(0, result.Length - floatPart.Length);
        result.TrimEnd('0');
        return result;
    }
    
    private void DisableSpecialStats()
    {
        foreach (var towerStat in specialTowerStats)
        {
            towerStat.gameObject.SetActive(false);
        }
    }

    private void SetBaseValues(Tower tower)
    {
        DisableSpecialStats();
        
        //Damage
        var upgradeDamage = tower.upgrades[0];
        var greenDamage = FloatToString(tower.GetDmg() - tower.Dmg, "0.0", ",0");
        baseTowerStats[0].SetData(upgradeDamage.UpgradeSprite, upgradeDamage.upgradeLabel, tower.Dmg.ToString(), greenDamage);
        //AttackSpeed
        var upgradeAttackSpeed = tower.upgrades[1];
        var greenAttackSpeed = FloatToString(tower.GetAttackSpeed() - tower.attackSpeed, "0.0", ",0");
        baseTowerStats[1].SetData(upgradeAttackSpeed.UpgradeSprite, upgradeAttackSpeed.upgradeLabel, tower.attackSpeed.ToString(), greenAttackSpeed);
        //ShootAngle
        var upgradeShootAngle = tower.upgrades[2];
        var greenShootAngle = FloatToString(tower.GetShootAngle() - tower.shootAngle, "0.0", ",0");
        baseTowerStats[2].SetData(upgradeShootAngle.UpgradeSprite, upgradeShootAngle.upgradeLabel, tower.shootAngle.ToString(), greenShootAngle);
        //ShootDistance
        var upgradeShootDistance = tower.upgrades[3];
        var greenShootDistance = FloatToString(tower.GetShootDistance() - tower.shootDistance, "0.0", ",0");
        baseTowerStats[3].SetData(upgradeShootDistance.UpgradeSprite, upgradeShootDistance.upgradeLabel, tower.shootDistance.ToString(), greenShootDistance);
    }

    private void SetFlamethrowerValues(Flamethrower tower)
    {
        DisableSpecialStats();
        
        //BurnDamage
        var upgradeBurnDamage = tower.upgrades[4];
        specialTowerStats[0].gameObject.SetActive(true);
        var greenBurnDamage = FloatToString(tower.burnDmg * tower.burnDmgMultiplier - tower.burnDmg, "0.0", ",0");
        specialTowerStats[0].SetData(upgradeBurnDamage.UpgradeSprite, upgradeBurnDamage.upgradeLabel, tower.burnDmg.ToString(), greenBurnDamage);
        
        //BurnTime
        var upgradeBurnTime = tower.upgrades[5];
        specialTowerStats[1].gameObject.SetActive(true);
        var greenBurnTime = FloatToString(tower.burnTime * tower.burnTimeMultiplier - tower.burnTime, "0.0", ",0");
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
        var greenTimeToFreeze = FloatToString(totalTimeToFreeze - whiteTimeToFreeze, "0.0", ",0");
        specialTowerStats[0].SetData(upgradeBurnDamage.UpgradeSprite, upgradeBurnDamage.upgradeLabel, FloatToString(whiteTimeToFreeze, "0.0", ",0"), greenTimeToFreeze);
        
        //FreezeTime
        var upgradeBurnTime = tower.upgrades[5];
        specialTowerStats[1].gameObject.SetActive(true);
        var greenBurnTime = FloatToString(tower.freezeTime * tower.freezeTimeMultiplier - tower.freezeTime, "0.0", ",0");
        specialTowerStats[1].SetData(upgradeBurnTime.UpgradeSprite, upgradeBurnTime.upgradeLabel, tower.freezeTime.ToString(), greenBurnTime);
    }

    private void SetLaserValues(Laser tower)
    {
        //CoolDelay
        var upgradeCoolDelay = tower.upgrades[4];
        specialTowerStats[0].gameObject.SetActive(true);
        var greenCoolDelay = FloatToString(tower.coolDelay * tower.coolDelayMultiplier - tower.coolDelay, "0.0", ",0");
        specialTowerStats[0].SetData(upgradeCoolDelay.UpgradeSprite, upgradeCoolDelay.upgradeLabel, tower.coolDelay.ToString(), greenCoolDelay);
        
        //HeatBonus
        var upgradeHeatBonus = tower.upgrades[5];
        specialTowerStats[1].gameObject.SetActive(true);
        var greenHeatBonus = FloatToString(tower.multiplierPerHeatStack * tower.multiplierPerHeatStackMultiplier - tower.multiplierPerHeatStack, "0.00", ",00");
        specialTowerStats[1].SetData(upgradeHeatBonus.UpgradeSprite, upgradeHeatBonus.upgradeLabel, tower.multiplierPerHeatStack.ToString(), greenHeatBonus);
        
        //MaxHeat
        var upgradeMaxHeat = tower.upgrades[6];
        specialTowerStats[2].gameObject.SetActive(true);
        var greenMaxHeat = FloatToString(tower.maxHeat * tower.maxHeatMultiplier - tower.maxHeat, "0.0", ",0");
        specialTowerStats[2].SetData(upgradeMaxHeat.UpgradeSprite, upgradeMaxHeat.upgradeLabel, tower.maxHeat.ToString(), greenMaxHeat);
    }
    
    private void SetMinigunValues(Minigun tower)
    {
        //CoolDelay
        var upgradeCoolDelay = tower.upgrades[4];
        specialTowerStats[0].gameObject.SetActive(true);
        var greenCoolDelay = FloatToString(tower.coolDelay * tower.coolDelayMultiplier - tower.coolDelay, "0.0", ",0");
        specialTowerStats[0].SetData(upgradeCoolDelay.UpgradeSprite, upgradeCoolDelay.upgradeLabel, tower.coolDelay.ToString(), greenCoolDelay);
        
        //HeatBonus
        var upgradeHeatBonus = tower.upgrades[5];
        specialTowerStats[1].gameObject.SetActive(true);
        var greenHeatBonus = FloatToString(tower.bonusAttackSpeedPerHeat * tower.bonusAttackSpeedPerHeatMultiplier - tower.bonusAttackSpeedPerHeat, "0.00", ",00");
        specialTowerStats[1].SetData(upgradeHeatBonus.UpgradeSprite, upgradeHeatBonus.upgradeLabel, tower.bonusAttackSpeedPerHeat.ToString(), greenHeatBonus);
        
        //MaxHeat
        var upgradeMaxHeat = tower.upgrades[6];
        specialTowerStats[2].gameObject.SetActive(true);
        var greenMaxHeat = FloatToString(tower.maxHeat * tower.maxHeatMultiplier - tower.maxHeat, "0.0", ",0");
        specialTowerStats[2].SetData(upgradeMaxHeat.UpgradeSprite, upgradeMaxHeat.upgradeLabel, tower.maxHeat.ToString(), greenMaxHeat);
    }
    
    private void SetMortarValues(Mortar tower)
    {
        //ExplosionRadius
        var upgradeExplosionRadius = tower.upgrades[4];
        specialTowerStats[0].gameObject.SetActive(true);
        var greenExplosionRadius = FloatToString(tower.explosionRadius * tower.explosionRadiusMultiplier - tower.explosionRadius, "0.0", ",0");
        specialTowerStats[0].SetData(upgradeExplosionRadius.UpgradeSprite, upgradeExplosionRadius.upgradeLabel, tower.explosionRadius.ToString(), greenExplosionRadius);
    }
    
    private void SetRailgunValues(Railgun tower)
    {
        DisableSpecialStats();
        
        //DamageDecrease
        var upgradeDamageDecrease = tower.upgrades[4];
        specialTowerStats[0].gameObject.SetActive(true);
        var greenDamageDecrease = FloatToString(tower.dmgMultiplier * tower.dmgMultiplierMultiplier - tower.dmgMultiplier, "0.00", ",00");
        specialTowerStats[0].SetData(upgradeDamageDecrease.UpgradeSprite, upgradeDamageDecrease.upgradeLabel, tower.dmgMultiplier.ToString(), greenDamageDecrease);
        
        //MinDamage
        var upgradeMinDamage = tower.upgrades[5];
        specialTowerStats[1].gameObject.SetActive(true);
        var greenMinDamage = FloatToString(tower.minDmg * tower.minDmgMultiplier - tower.minDmg, "0.0", ",0");
        specialTowerStats[1].SetData(upgradeMinDamage.UpgradeSprite, upgradeMinDamage.upgradeLabel, tower.minDmg.ToString(), greenMinDamage);
    }
    
    private void SetShotgunValues(Shotgun tower)
    {
        //Bullet
        var upgradeBullet = tower.upgrades[4];
        specialTowerStats[0].gameObject.SetActive(true);
        var greenBullet = tower.bonusBullets.ToString();
        specialTowerStats[0].SetData(upgradeBullet.UpgradeSprite, upgradeBullet.upgradeLabel, tower.bulletCount.ToString(), greenBullet);
    }
    
    private void SetTeslaValues(Tesla tower)
    {
        //LightningCount
        var upgradeLightningCount = tower.upgrades[4];
        specialTowerStats[0].gameObject.SetActive(true);
        var greenLightningCount = tower.bonusLightningCount.ToString();
        specialTowerStats[0].SetData(upgradeLightningCount.UpgradeSprite, upgradeLightningCount.upgradeLabel, tower.lightningCount.ToString(), greenLightningCount);
        
        //DamageDecrease
        var upgradeDamageDecrease = tower.upgrades[5];
        specialTowerStats[1].gameObject.SetActive(true);
        var greenDamageDecrease = FloatToString(tower.dmgDecrease * tower.dmgDecreaseMultiplier - tower.dmgDecrease, "0.00", ",00");
        specialTowerStats[1].SetData(upgradeDamageDecrease.UpgradeSprite, upgradeDamageDecrease.upgradeLabel, tower.dmgDecrease.ToString(), greenDamageDecrease);
        
        //JumpDistance
        var upgradeJumpDistance = tower.upgrades[6];
        specialTowerStats[2].gameObject.SetActive(true);
        var greenJumpDistance = FloatToString(tower.lightningJumpDistance * tower.lightningJumpDistanceMultiplier - tower.lightningJumpDistance, "0.0", ",0");
        specialTowerStats[2].SetData(upgradeJumpDistance.UpgradeSprite, upgradeJumpDistance.upgradeLabel, tower.lightningJumpDistance.ToString(), greenJumpDistance);
    }
}
