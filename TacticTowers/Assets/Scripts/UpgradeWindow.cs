using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] private List<GameObject> upgradeButtons;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void InitializeUpgrade(Tower tower)
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
    }
}