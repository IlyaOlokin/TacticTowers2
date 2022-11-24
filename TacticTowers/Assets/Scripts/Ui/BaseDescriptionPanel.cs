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
    [SerializeField] private Image baseActiveAbilityImage;
    [SerializeField] private Base defaultBase;

    private void Start()
    {
        GetBaseInfo(BaseSelectManager.SelectedBase == null 
            ? defaultBase 
            : BaseSelectManager.SelectedBase.GetComponent<Base>());
    }

    public void GetBaseInfo(Base _base)
    {
        activeDescription.text = "Активная способнсть: " + _base.gameObject.GetComponent<BaseActive>().description;
        passiveDescription.text = "Пассивная способность: " + _base.gameObject.GetComponent<BasePassive>().description;
        baseImage.sprite = _base.baseImage;
        baseActiveAbilityImage.sprite = _base.GetComponent<BaseActive>().activeAbilitySprite;
    }
}
