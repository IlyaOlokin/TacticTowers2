using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private List<Transform> upgradeButtonsPositions;

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

        Button.onClick.AddListener(() => CreateNewTower(towerTypes[upgradeIndex], tower));
        Button.onClick.AddListener(() => gameObject.SetActive(false));
        Button.onClick.AddListener(() => FindObjectOfType<AudioManager>().Play("ButtonClick1"));
        
        button.GetComponent<UpgradeButton>().ActivateSuperCardEffects(false);

        var towerComponent = towerTypes[upgradeIndex].GetComponent<Tower>();
        upgradeButton.upgradeLabel.GetComponent<TextLocaliser>().SetKey(towerComponent.towerName);
        upgradeButton.upgradeText.GetComponent<TextLocaliser>().SetKey(towerComponent.towerDescription);
        upgradeButton.upgradeImage.sprite = towerComponent.towerSprites[0];
    }
    
    private void InitializeSpecialUpgrade(Tower tower, out int specialUpgradesLeft)
    {
        specialUpgradesLeft = tower.specialUpgrades.Count - tower.upgradedSpecilaUpgrades.Count(t => t);
        for (int i = 0; i < specialUpgradesLeft; i++)
        {
            InitializeSpecialUpgradeButton(upgradeButtons[i], tower, i);
        }
    }

    private void InitializeSpecialUpgradeButton(GameObject button, Tower tower, int upgradeIndex)
    {
        var Button = button.GetComponent<Button>();
        Button.onClick.RemoveAllListeners();
        var upgradeButton = button.GetComponent<UpgradeButton>();
        var upgrade = GetNextSpecialUpgrade(tower, upgradeIndex);

        Button.onClick.AddListener(() => upgrade.Execute(tower));
        Button.onClick.AddListener(() => gameObject.SetActive(false));
        Button.onClick.AddListener(() => FindObjectOfType<AudioManager>().Play("ButtonClick1"));
        Button.onClick.AddListener(EnableAllUpgradeButtons);
        
        button.GetComponent<UpgradeButton>().ActivateSuperCardEffects(false);
        //upgradeButton.upgradeLabel.GetComponent<TextLocaliser>().SetKey(upgrade.upgradeLabel);
        //upgradeButton.upgradeText.GetComponent<TextLocaliser>().SetKey(upgrade.GetUpgradeText());
        
        upgradeButton.upgradeLabel.text = upgrade.upgradeLabel;
        upgradeButton.upgradeText.text = upgrade.GetUpgradeText();
        upgradeButton.upgradeImage.sprite = tower.towerSprites[GetSpecialUpgradeLevel(tower)];
    }

    private void SetUpgradeButtonsSpecialPositions(int specialUpgradesLeft)
    {
        switch (specialUpgradesLeft)
        {
            case 3:
                SetDefaultUpgradeButtonsPositions();
                break;
            case 2:
                upgradeButtons[0].transform.position = upgradeButtonsPositions[3].transform.position;
                upgradeButtons[1].transform.position = upgradeButtonsPositions[4].transform.position;
                break;
            case 1:
                upgradeButtons[0].transform.position = upgradeButtonsPositions[1].transform.position;
                break;
        }
    }

    private void SetDefaultUpgradeButtonsPositions()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].transform.position = upgradeButtonsPositions[i].transform.position;
        }
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
        upgradingTowerImage.sprite = tower.towerSprites[GetSpecialUpgradeLevel(tower)];
        upgradingTowerImage.transform.rotation = Quaternion.Euler(0,0,tower.shootDirection - 90);
    }

    private SpecialUpgrade GetNextSpecialUpgrade(Tower tower, int upgradeIndex)
    {
        for (int i = 0; i < tower.specialUpgrades.Count; i++)
        {
            if (tower.upgradedSpecilaUpgrades[i]) continue;
            if (upgradeIndex == 0)  return tower.specialUpgrades[i];
            upgradeIndex--;
        }

        return null;
    }

    private int GetSpecialUpgradeLevel(Tower tower)
    {
        var towerUpgradeLevel = tower.upgradeLevel;
        int result = -1;
        for (int i = 0; i < specialUpgradesLevels.Count; i++)
        {
            if (towerUpgradeLevel < specialUpgradesLevels[i])
            {
                break;
            }
            result = i;
        }
        
        return result + 1;
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
        ChangeButtonsVisual(typeUpgradeSprite, upgradeText);
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
        }
        label.GetComponent<TextLocaliser>().SetKey(labelText);
    }
}