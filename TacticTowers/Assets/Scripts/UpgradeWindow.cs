using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeWindow : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private Text label;
    [SerializeField] private String typeUpgradeText;
    [SerializeField] private String upgradeText;
    [SerializeField] private Image upgradingTowerImage;

    [SerializeField] private Sprite typeUpgradeSprite;
    [SerializeField] private Sprite upgradeSprite;
    
    [Header("Functionality")]
    [SerializeField] private int towerTypeUpgradeLevel;
    [SerializeField] private List<int> specialUpgradesLevels;
    [SerializeField] private float superUpgradeChance;

    [SerializeField] private List<GameObject> upgradeButtons;

    [SerializeField] private List<GameObject> towerTypes;
    [SerializeField] private List<GameObject> unlockableTowerTypes;

    [SerializeField] private AudioMixer audioMixer;

    [NonSerialized] public TowerDrag td;
    [NonSerialized] public TowerUpgrade tu;

    private void OnEnable()
    {
        TimeManager.Pause(audioMixer);
        
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
        TimeManager.Resume(audioMixer);
    }

    public void UpgradeTower(Tower tower)
    {
        if (tower.upgradeLevel == towerTypeUpgradeLevel)
        {
            InitializeTypeUpgrade(tower);
            ChangeVisualOnTowerTypeUpgrade();
        }
        else if (specialUpgradesLevels.Contains(tower.upgradeLevel))
        {
            InitializeSpecialUpgrade(tower, out int specialUpgradesLeft);
            ChangeVisualOnTowerSpecialUpgrade(specialUpgradesLeft);
        }
        else
        {
            InitializeUpgrade(tower);
            ChangeVisualOnTowerUpgrade();
        }
        ShowUpgradingTower(tower);
    }
    
    private void InitializeSpecialUpgrade(Tower tower, out int specialUpgradesLeft)
    {
        specialUpgradesLeft = specialUpgradesLevels.Count - specialUpgradesLevels.IndexOf(tower.upgradeLevel);
        for (int i = 0; i < specialUpgradesLeft; i++)
        {
            InitializeSpecialUpgradeButton(upgradeButtons[i], tower, i, specialUpgradesLeft);
        }
    }

    private void InitializeSpecialUpgradeButton(GameObject button, Tower tower, int upgradeIndex, int specialUpgradesLeft)
    {
        var Button = button.GetComponent<Button>();
        Button.onClick.RemoveAllListeners();
        var upgradeButton = button.GetComponent<UpgradeButton>();
        var upgrade = GetNextSpecialUpgrade(tower, upgradeIndex);

        Button.onClick.AddListener(() => upgrade.Execute(tower));
        Button.onClick.AddListener(() => gameObject.SetActive(false));
        Button.onClick.AddListener(() => FindObjectOfType<AudioManager>().Play("ButtonClick1"));
        Button.onClick.AddListener(EnableAllUpgradeButtons);

        //upgradeButton.upgradeLabel.GetComponent<TextLocaliser>().SetKey(upgrade.upgradeLabel);
        //upgradeButton.upgradeText.GetComponent<TextLocaliser>().SetKey(upgrade.GetUpgradeText());
        
        upgradeButton.upgradeLabel.text = upgrade.upgradeLabel;
        upgradeButton.upgradeText.text = upgrade.GetUpgradeText();
        upgradeButton.upgradeImage.sprite = upgrade.UpgradeSprite;
    }

    private void SetUpgradeButtonsSpecialPositions(int specialUpgradesLeft)
    {
        
    }

    private void SetDefaultUpgradeButtonsPositions()
    {
        
    }

    private void DisableExtraUpgradesButtons(int specialUpgradesLeft)
    {
        for (int i = upgradeButtons.Count - 1; i >= specialUpgradesLeft; i--)
        {
            upgradeButtons[i].SetActive(false);
        }
    }

    private void EnableAllUpgradeButtons()
    {
        foreach (var button in upgradeButtons)
        {
            button.SetActive(true);
        }
    }

    private void ShowUpgradingTower(Tower tower)
    {
        upgradingTowerImage.sprite = tower.towerSprite;
        upgradingTowerImage.transform.rotation = Quaternion.Euler(0,0,tower.shootDirection - 90);
    }

    private SpecialUpgrade GetNextSpecialUpgrade(Tower tower, int upgradeIndex)
    {
        for (int i = 0; i < tower.specialUpgrades.Count; i++)
        {
            if (tower.specialUpgrades[i].isUpgraded) continue;
            if (upgradeIndex == 0)  return tower.specialUpgrades[i];
            upgradeIndex--;
        }

        return null;
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
                InitializeUpgradeButton(upgradeButtons[i], tower, upgradeIndex);
            }
            else i--;
        }
    }

    private void InitializeUpgradeButton(GameObject button, Tower tower, int upgradeIndex)
    {
        var Button = button.GetComponent<Button>();
        Button.onClick.RemoveAllListeners();
        var upgradeButton = button.GetComponent<UpgradeButton>();
        var upgrade = tower.upgrades[upgradeIndex];
        
        float chanceToSuper = Random.Range(0f, 1f);
        bool isSuper = chanceToSuper < superUpgradeChance;
        upgrade.ApplyBonusIncrement(isSuper);
        button.GetComponent<UpgradeButton>().ActivateSuperCardEffects(isSuper);
        
        Button.onClick.AddListener(() => upgrade.Execute(tower));
        Button.onClick.AddListener(() => gameObject.SetActive(false));
        Button.onClick.AddListener(() => FindObjectOfType<AudioManager>().Play("ButtonClick1"));

        upgradeButton.upgradeLabel.GetComponent<TextLocaliser>().SetKey(upgrade.upgradeLabel);
        upgradeButton.upgradeText.GetComponent<TextLocaliser>().SetKey(upgrade.GetUpgradeText());
        upgradeButton.upgradeText.text = string.Format(upgradeButton.upgradeText.text, isSuper ? upgrade.GetBonusForFormatting() * 2 : upgrade.GetBonusForFormatting());// upgrade.FormatUpgradeText(isSuper);
        upgradeButton.upgradeImage.sprite = upgrade.UpgradeSprite;
    }

    private void InitializeTypeUpgrade(Tower tower)
    {
        List<int> pickedIndexes = new List<int>();
        
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
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
        Button.onClick.RemoveAllListeners();
        var upgradeButton = button.GetComponent<UpgradeButton>();
        
        button.GetComponent<UpgradeButton>().ActivateSuperCardEffects(false);
        
        Button.onClick.AddListener(() => CreateNewTower(towerTypes[upgradeIndex], tower));
        Button.onClick.AddListener(() => gameObject.SetActive(false));
        Button.onClick.AddListener(() => FindObjectOfType<AudioManager>().Play("ButtonClick1"));

        var towerComponent = towerTypes[upgradeIndex].GetComponent<Tower>();
        upgradeButton.upgradeLabel.GetComponent<TextLocaliser>().SetKey(towerComponent.towerName);
        upgradeButton.upgradeText.GetComponent<TextLocaliser>().SetKey(towerComponent.towerDescription);
        upgradeButton.upgradeImage.sprite = towerComponent.towerSprite;
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
        ChangeButtonsVisual(typeUpgradeSprite, typeUpgradeText);
        SetDefaultUpgradeButtonsPositions();
    }
    
    private void ChangeVisualOnTowerSpecialUpgrade(int specialUpgradesLeft)
    {
        DisableExtraUpgradesButtons(specialUpgradesLeft);
        SetUpgradeButtonsSpecialPositions(specialUpgradesLeft);
    }
    
    private void ChangeVisualOnTowerUpgrade()
    {
        ChangeButtonsVisual(upgradeSprite, upgradeText);
        SetDefaultUpgradeButtonsPositions();
    }

    private void ChangeButtonsVisual(Sprite imageSprite, string labelText)
    {
        foreach (var button in upgradeButtons)
        {
            button.GetComponent<UpgradeButton>().ChangeSprite(imageSprite);
            label.GetComponent<TextLocaliser>().SetKey(labelText);
        }
    }
}