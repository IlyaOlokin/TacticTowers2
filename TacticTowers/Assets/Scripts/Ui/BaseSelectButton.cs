using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSelectButton : MonoBehaviour
{
    [SerializeField] private SelectIndicator selectIndicator;
    [SerializeField] private GameObject Base;
    [SerializeField] private BaseDescriptionPanel baseDescription;
    [SerializeField] private Image baseImage;

    private void Start()
    {
        baseImage.sprite = Base.GetComponent<Base>().baseImage;
    }

    public void SelectBase()
    {
        GameManager.SelectedBase = Base;
        var _base = Base.GetComponent<Base>();
        baseDescription.GetBaseInfo(_base);
        selectIndicator.GetNewDestination(transform.position);
    } 
}
