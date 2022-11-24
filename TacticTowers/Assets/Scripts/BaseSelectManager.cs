using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSelectManager : MonoBehaviour
{
    public static GameObject SelectedBase;
    public static int SelectedBaseIndex;

    private List<bool> baseLock;

    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private List<GameObject> bases;
    
    [SerializeField] private SelectIndicator selectIndicator;
    [SerializeField] private BaseDescriptionPanel baseDescription;
    
    void Awake()
    {
        LoadBaseLocks();
        InitializeButtons();
        SelectBase(SelectedBaseIndex);
    }

    private void LoadBaseLocks()
    {
        var s = DataLoader.LoadString("BaseLocks", "10000000");
        baseLock = new List<bool>();
        for (var i = 0; i < s.Length; i++)
        {
            char _char = s[i];
            baseLock.Add(_char == '0');
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < Math.Min(buttons.Count, bases.Count); i++)
        {
            var button = buttons[i].GetComponent<BaseSelectButton>();
            button.index = i;
            button.BaseSelectManager = this;
            button.baseSprite = bases[i].GetComponent<Base>().baseImage;
            button.lockGameObject.SetActive(baseLock[i]);
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
