using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelChanger : MonoBehaviour
{
    [SerializeField] private int panelNum;
    [SerializeField] private TutorialPanelManager tpm;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            tpm.SetCollidedPanelNum(panelNum);
        }
    }
}
