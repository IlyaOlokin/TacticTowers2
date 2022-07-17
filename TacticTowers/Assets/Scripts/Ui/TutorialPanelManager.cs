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
    private int colpan;
    public void col(int num)
    {
        colpan = num;
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
            case 5 when colpan == 5:
                panels[4].SetActive(true);
                colpan = 0;
                //collided = false;
                break;
            case 6 when colpan == 6:
                panels[5].SetActive(true);
                // = false;
                colpan = 0;
                break;
            case 7 when colpan == 7:
                if (waveCount[0] == waveCount[1])
                    panels[6].SetActive(true);
                //collided = false;
                colpan = 0;
                break;
            case 8 when enemies.transform.childCount == 0:
                panels[7].SetActive(true);
                break;
            case 9 when enemies.transform.childCount == 0:
                //ar waveCount = waveText.text.Split('/').Select(int.Parse).ToArray();
//
                //if (waveCount[0] == waveCount[1])
                    panels[8].SetActive(true);
                
                break;
            case 10:
                if (towerLevels.Select(t => t.text).Select(int.Parse).Any(l => l > 1) && !upgradeWindow.activeInHierarchy)
                    panels[9].SetActive(true);
                break;
        }
        //if (enemies.transform.childCount == 0)
        //{
        //    var waveCount = waveText.text.Split('/').Select(int.Parse).ToArray();
//
        //    if (waveCount[0] == waveCount[1] && !was4thOpen)
        //    {
        //        panels[2].SetActive(true);
        //        was4thOpen = true;
        //    }
        //}
        //
        //if (towerLevels.Select(t => t.text).Select(int.Parse).Any(l => l > 1) && !upgradeWindow.activeInHierarchy)
        //    panels[3].SetActive(true);
    }
}
