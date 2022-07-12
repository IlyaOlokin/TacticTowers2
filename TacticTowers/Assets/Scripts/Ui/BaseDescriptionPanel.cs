using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Text activeDescription;
    [SerializeField] private Text passiveDescription;
    [SerializeField] private Image baseImage;
    [SerializeField] private Base defaultBase;

    private void Start()
    {
        GetBaseInfo(GameManager.SelectedBase == null 
            ? defaultBase 
            : GameManager.SelectedBase.GetComponent<Base>());
    }

    public void GetBaseInfo(Base _base)
    {
        activeDescription.text = "Активная способнсть: " + _base.activeDescription;
        passiveDescription.text = "Пассивная способность: " + _base.passiveDescription;
        baseImage.sprite = _base.baseImage;
    }
}
