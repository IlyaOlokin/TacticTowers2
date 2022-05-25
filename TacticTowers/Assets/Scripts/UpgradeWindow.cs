using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeWindow : MonoBehaviour
{
    [Header("Visual")] 
    [SerializeField] private Color typeUpgradeColor;
    [SerializeField] private Color upgradeColor;
    [SerializeField] private Color typeUpgradeTextColor;
    [SerializeField] private Color upgradeTextColor;
    [SerializeField] private Text label;
    [SerializeField] private String typeUpgradeText;
    [SerializeField] private String upgradeText;
    [SerializeField] private Image upgradingTowerImage;
    
    [Header("Functionality")]
    [SerializeField] private List<GameObject> upgradeButtons;

    [SerializeField] private int towerTypeUpgradeLevel;
    [SerializeField] private List<GameObject> towerTypes;
    [SerializeField] private List<GameObject> unlockableTowerTypes;

    [NonSerialized] public TowerDrag td;
    [NonSerialized] public TowerUpgrade tu;

    private void OnEnable()
    {
        Time.timeScale = 0;
        AddUnlockedTowerTypes();
    }

    private void AddUnlockedTowerTypes()
    {
        if (Technologies.IsFrostGunUnlocked && !towerTypes.Contains(unlockableTowerTypes[0]))
            towerTypes.Add(unlockableTowerTypes[0]);
        if (Technologies.IsFlamethrowerUnlocked && !towerTypes.Contains(unlockableTowerTypes[1]))
            towerTypes.Add(unlockableTowerTypes[1]);
        if (Technologies.IsRailgunUnlocked && !towerTypes.Contains(unlockableTowerTypes[2]))
            towerTypes.Add(unlockableTowerTypes[2]);
        if (Technologies.IsTeslaUnlocked && !towerTypes.Contains(unlockableTowerTypes[3]))
            towerTypes.Add(unlockableTowerTypes[3]);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void UpgradeTower(Tower tower)
    {
        if (tower.upgradeLevel == towerTypeUpgradeLevel)
        {
            InitializeTypeUpgrade(tower);
            ChangeVisualOnTowerTypeUpgrade();
        }
        else
        {
            InitializeUpgrade(tower);
            ChangeVisualOnTowerUpgrade();
        }
        ShowUpgradingTower(tower);
    }

    private void ShowUpgradingTower(Tower tower)
    {
        upgradingTowerImage.sprite = tower.towerSprite;
        upgradingTowerImage.transform.rotation = Quaternion.Euler(0,0,tower.shootDirection - 90);
    }

    private void InitializeUpgrade(Tower tower)
    {
        List<int> pickedIndexes = new List<int>();
        
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            var upgradeIndex = Random.Range(0, tower.upgrades.Count);
            if (!pickedIndexes.Contains(upgradeIndex))
            {
                pickedIndexes.Add(upgradeIndex);
                InitializeButton(upgradeButtons[i], tower, upgradeIndex);
            }
            else i--;
        }
    }

    private void InitializeButton(GameObject button, Tower tower, int upgradeIndex)
    {
        var Button = button.GetComponent<Button>();
        var upgradeButton = button.GetComponent<UpgradeButton>();
        var upgrade = tower.upgrades[upgradeIndex];
        
        Button.onClick.AddListener(() => upgrade.Execute(tower));
        Button.onClick.AddListener(() => gameObject.SetActive(false));
        upgradeButton.upgradeLabel.text = upgrade.upgradeLabel;
        upgradeButton.upgradeText.text = upgrade.upgradeText;
        upgradeButton.upgradeImage.sprite = upgrade.UpgradeSprite;
    }

    private void InitializeTypeUpgrade(Tower tower)
    {
        List<int> pickedIndexes = new List<int>();
        
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            var upgradeIndex = Random.Range(0, towerTypes.Count);
            if (!pickedIndexes.Contains(upgradeIndex))
            {
                pickedIndexes.Add(upgradeIndex);
                
                InitializeTypeUpgradeButton(upgradeButtons[i], tower, upgradeIndex);
            }
            else i--;
        }
    }
    
    private void InitializeTypeUpgradeButton(GameObject button, Tower tower, int upgradeIndex)
    {
        var Button = button.GetComponent<Button>();
        var upgradeButton = button.GetComponent<UpgradeButton>();
        //var upgrade = tower.upgrades[upgradeIndex];

        Button.onClick.AddListener(() => CreateNewTower(towerTypes[upgradeIndex], tower));
        Button.onClick.AddListener(() => gameObject.SetActive(false));
        upgradeButton.upgradeLabel.text = towerTypes[upgradeIndex].GetComponent<Tower>().towerName;
        upgradeButton.upgradeText.text =  towerTypes[upgradeIndex].GetComponent<Tower>().towerDescription;
        upgradeButton.upgradeImage.sprite = towerTypes[upgradeIndex].GetComponent<Tower>().towerSprite;
    }

    private void CreateNewTower(GameObject towerType, Tower tower)
    {
        var newTower = Instantiate(towerType, tower.transform.position, quaternion.identity);
        var newTowerComp = newTower.GetComponent<Tower>();
        newTowerComp.GetMultipliers(tower);
        newTowerComp.ReplaceTower(tower);
        tu.tower = newTowerComp;
        td.tower = newTowerComp;
        newTowerComp.shootZone.DrawShootZone();

        Destroy(tower.gameObject);
    }

    private void ChangeVisualOnTowerTypeUpgrade()
    {
        ChangeButtonsVisual(typeUpgradeColor, typeUpgradeTextColor, typeUpgradeText);
    }
    private void ChangeVisualOnTowerUpgrade()
    {
        ChangeButtonsVisual(upgradeColor, upgradeTextColor, upgradeText);
    }

    private void ChangeButtonsVisual(Color imageColor, Color textColor, string labelText)
    {
        foreach (var button in upgradeButtons)
        {
            button.GetComponent<Image>().color = imageColor;
            button.GetComponent<UpgradeButton>().upgradeLabel.color = textColor;
            button.GetComponent<UpgradeButton>().upgradeText.color = textColor;
            label.text = labelText;
        }
    }
}