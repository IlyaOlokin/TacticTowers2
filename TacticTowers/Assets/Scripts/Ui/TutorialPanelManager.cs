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
    [SerializeField] private List<GameObject> normalTowers;
    [SerializeField] private List<Tower> upgradedTowers;
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
                break;
            case 3:
                if (collidedPanelNum == 2 && enemies.transform.childCount == 0)
                {
                    panelChangers[1].SetActive(true);
                }
                else if (collidedPanelNum == 3 && waveCount[0] == 2)
                {
                    panels[2].SetActive(true);
                    panelChangers[1].SetActive(false);
                }
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
                if (towerLevels.Select(t => t.text).Select(int.Parse).Any(l => l > 1) &&
                    !upgradeWindow.activeInHierarchy)
                {
                    StartCoroutine(nameof(WaitForShootZones));
                    panels[6].SetActive(true);
                    foreach (var tower in normalTowers)
                        tower.SetActive(false);
                    foreach (var tower in upgradedTowers)
                    {
                        foreach (var specialUpgrade in tower.specialUpgrades)
                            specialUpgrade.Execute(tower);
                        tower.upgradeLevel = 20;
                        tower.transform.parent.gameObject.SetActive(true);
                    }
                }
                break;
            case 8:
                //if (base)
                break;
        }
    }
    private IEnumerator WaitForShootZones()
    {
        yield return new WaitForSeconds(2.0f);
    }
    
    private void Awake()
    {
        CurrentPanel = 1;
    }
}
