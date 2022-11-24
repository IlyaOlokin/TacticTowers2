using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSelectManager : MonoBehaviour
{
    public static GameObject SelectedBase;
    public static int SelectedBaseIndex;

    private List<bool> baseUnlock;

    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private List<GameObject> bases;
    
    [SerializeField] private SelectIndicator selectIndicator;
    [SerializeField] private BaseDescriptionPanel baseDescription;
    [SerializeField] private Button playButton;
    public GameObject unlockBaseButton;
    
    void Awake()
    {
        LoadBaseLocks();
        InitializeButtons();
        SelectBase(SelectedBaseIndex);
    }

    private void LoadBaseLocks()
    {
        var s = DataLoader.LoadString("BaseUnlocks", "10000000");
        baseUnlock = new List<bool>();
        for (var i = 0; i < s.Length; i++)
        {
            char _char = s[i];
            baseUnlock.Add(_char == '1');
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
            button.lockGameObject.SetActive(!baseUnlock[i]);
        }
    }

    public void SelectBase(int index)
    {
        SelectedBase = bases[index];
        SelectedBaseIndex = index;
        DataLoader.SaveInt("selectedBaseIndex", SelectedBaseIndex);
        baseDescription.GetBaseInfo(bases[index].GetComponent<Base>());
        unlockBaseButton.SetActive(!baseUnlock[index]);
        selectIndicator.GetNewDestination(buttons[index].transform.position);
        playButton.interactable = baseUnlock[index];
    }

    public void OnBaseUnlock()
    {
        int cost = SelectedBase.GetComponent<Base>().unlockCost;
        if (Credits.credits < cost) return;
        
        Credits.TakeCredits(cost);
        baseUnlock[SelectedBaseIndex] = true;
        DataLoader.SaveString("BaseUnlocks", ConvertToSave());
        playButton.interactable = true;
        unlockBaseButton.SetActive(false);
        InitializeButtons();
    }

    private string ConvertToSave()
    {
        string result = "";
        for (int i = 0; i < baseUnlock.Count; i++)
        {
            result += baseUnlock[i] ? "1" : "0";
        }

        return result;
    }
}
