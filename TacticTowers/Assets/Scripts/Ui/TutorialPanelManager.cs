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
    [SerializeField] private List<GameObject> panelChangers;
    [SerializeField] private GameObject enemies;
    [SerializeField] private Text waveText;
    [SerializeField] private GameObject upgradeWindow;
    [SerializeField] private List<Text> towerLevels;
    
    [SerializeField] private Base _base;
    [SerializeField] private GameObject baseAbilityMenu;
    [SerializeField] private Button baseAbilityButton;
    [SerializeField] private Image baseAbilityCoolDownImage;
    [SerializeField] private Text baseCoolDownText;
    private int collidedPanelNum;
    
    public void SetCollidedPanelNum(int panelNum)
    {
        collidedPanelNum = panelNum;
    }
    
    private void Update()
    {
        var waveCount = waveText.text.Split('/').Select(int.Parse).ToArray();
        if (waveCount[0] == waveCount[1])
            panelChangers[1].SetActive(true);
        
        switch (CurrentPanel)
        {
            case 1:
                panels[0].SetActive(true);
                break;
            case 2 when collidedPanelNum == 2:
                panels[1].SetActive(true);
                panelChangers[0].SetActive(false);
                collidedPanelNum = 0;
                break;
            case 3 when collidedPanelNum == 3:
                if (waveCount[0] == waveCount[1])
                    panels[2].SetActive(true);
                panelChangers[1].SetActive(false);
                break;
            case 4:
                panels[3].SetActive(true);
                break;
            case 5 when enemies.transform.childCount == 0:
                panels[4].SetActive(true);
                collidedPanelNum = 0;
                break;
            case 6 when enemies.transform.childCount == 0:
                panels[5].SetActive(true);
                break;
            case 7:
                if (towerLevels.Select(t => t.text).Select(int.Parse).Any(l => l > 1) && !upgradeWindow.activeInHierarchy)
                    panels[6].SetActive(true);
                break;
            /*
            case 8 when enemies.transform.childCount == 0:
                panels[7].SetActive(true);
                break;
            case 9 when enemies.transform.childCount == 0:
                panels[8].SetActive(true);
                break;
            case 10:
                if (towerLevels.Select(t => t.text).Select(int.Parse).Any(l => l > 1) && !upgradeWindow.activeInHierarchy)
                    panels[9].SetActive(true);
                break;*/
        }
    }

    private void Awake()
    {
        /*_base.ExecuteBasePassiveEffect();
        _base.baseAbilityMenu = baseAbilityMenu;
        _base.abilityButton = baseAbilityButton;
        _base.coolDownImage = baseAbilityCoolDownImage;
        _base.coolDownText = baseCoolDownText;*/
        CurrentPanel = 1;
    }
}
