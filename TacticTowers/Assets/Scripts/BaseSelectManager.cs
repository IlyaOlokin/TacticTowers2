using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSelectManager : MonoBehaviour
{
    public static GameObject SelectedBase;
    public static int SelectedBaseIndex;
    
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private List<GameObject> bases;
    
    [SerializeField] private SelectIndicator selectIndicator;
    [SerializeField] private BaseDescriptionPanel baseDescription;
    
    void Awake()
    {
        InitializeButtons();
        SelectBase(SelectedBaseIndex);
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < Math.Min(buttons.Count, bases.Count); i++)
        {
            var button = buttons[i].GetComponent<BaseSelectButton>();
            button.index = i;
            button.BaseSelectManager = this;
            button.baseSprite = bases[i].GetComponent<Base>().baseImage;
        }
    }

    public void SelectBase(int index)
    {
        SelectedBase = bases[index];
        SelectedBaseIndex = index;
        DataLoader.SaveInt("selectedBaseIndex", SelectedBaseIndex);
        baseDescription.GetBaseInfo(bases[index].GetComponent<Base>());
        selectIndicator.GetNewDestination(buttons[index].transform.position);
    }
}
