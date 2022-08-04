using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelManager : MonoBehaviour
{
    public static int CurrentPanel = 1;
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private GameObject enemies;
    [SerializeField] private Text waveText;
    [SerializeField] private GameObject upgradeWindow;
    [SerializeField] private List<Text> towerLevels;
    
    [SerializeField] private Base _base;
    [SerializeField] private GameObject baseAbilityMenu;
    [SerializeField] private Button abilityButton;
    [SerializeField] private Image baseAbilityCoolDownImage;
    
    private int collidedPanelNum;
    
    public void SetCollidedPanelNum(int panelNum)
    {
        collidedPanelNum = panelNum;
    }
    
    private void Update()
    {
        var waveCount = waveText.text.Split('/').Select(int.Parse).ToArray();
        switch (TutorialPanelManager.CurrentPanel)
        {
            case 1:
                panels[0].SetActive(true);
                break;
            case 2:
                panels[1].SetActive(true);
                break;
            case 3:
                panels[2].SetActive(true);
                break;
            case 4:
                panels[3].SetActive(true);
                break;
            case 5 when collidedPanelNum == 5:
                panels[4].SetActive(true);
                collidedPanelNum = 0;
                break;
            case 6 when collidedPanelNum == 6:
                panels[5].SetActive(true);
                collidedPanelNum = 0;
                break;
            case 7 when collidedPanelNum == 7:
                if (waveCount[0] == waveCount[1])
                    panels[6].SetActive(true);
                collidedPanelNum = 0;
                break;
            case 8 when enemies.transform.childCount == 0:
                panels[7].SetActive(true);
                break;
            case 9 when enemies.transform.childCount == 0:
                panels[8].SetActive(true);
                break;
            case 10:
                if (towerLevels.Select(t => t.text).Select(int.Parse).Any(l => l > 1) && !upgradeWindow.activeInHierarchy)
                    panels[9].SetActive(true);
                break;
        }
    }

    private void Awake()
    {
        _base.ExecuteBasePassiveEffect();
        _base.baseAbilityMenu = baseAbilityMenu;
        _base.abilityButton = abilityButton;
        _base.coolDownImage = baseAbilityCoolDownImage;
    }
}
