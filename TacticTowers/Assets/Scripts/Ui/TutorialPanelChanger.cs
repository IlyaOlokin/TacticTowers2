using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelChanger : MonoBehaviour
{
    private int counter = 0;
    private bool was4thOpen;
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private GameObject enemies;
    [SerializeField] private Text waveCount;
    [SerializeField] private Text rightTowerLevel;
    [SerializeField] private GameObject upgradeWindow;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            counter++;
            
            if (counter == 1)
                panels[0].SetActive(true);

            if (counter == 2)
                panels[1].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.transform.childCount == 0 && int.Parse(waveCount.text[0].ToString()) == 2 && !was4thOpen)
        {
            panels[2].SetActive(true);
            was4thOpen = true;
        }
        
        if (int.Parse(rightTowerLevel.text) == 2 && !upgradeWindow.activeInHierarchy)
            panels[3].SetActive(true);
    }
}
