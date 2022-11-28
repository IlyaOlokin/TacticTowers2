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
    [SerializeField] private Text costText;

    private void Start()
    {
        GetBaseInfo(BaseSelectManager.SelectedBase == null 
            ? defaultBase 
            : BaseSelectManager.SelectedBase.GetComponent<Base>());
    }

    public void GetBaseInfo(Base _base)
    {
        activeDescription.GetComponent<TextLocaliser>().SetKey(_base.gameObject.GetComponent<BaseActive>().description);
        passiveDescription.GetComponent<TextLocaliser>().SetKey(_base.gameObject.GetComponent<BasePassive>().description);
        baseImage.sprite = _base.baseImage;
        baseActiveAbilityImage.sprite = _base.GetComponent<BaseActive>().activeAbilitySprite;
        costText.text = _base.unlockCost.ToString();
    }
}
