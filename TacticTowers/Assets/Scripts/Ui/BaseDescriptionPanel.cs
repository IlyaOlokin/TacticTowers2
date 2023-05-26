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
    [SerializeField] private Image basePassiveAbilityImage;
    [SerializeField] private Image baseActiveAbilityImage;
    [SerializeField] private Base defaultBase;
    [SerializeField] private GameObject creditsUnlockObject;
    [SerializeField] private Text costText;
    [SerializeField] private GameObject trialUnlockObject;
    [SerializeField] private Text trialText;


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
        basePassiveAbilityImage.sprite = _base.baseImage;
        baseActiveAbilityImage.sprite = _base.GetComponent<BaseActive>().activeAbilitySprite;
        if (_base.isUnlockableFromTrial)
        {
            trialUnlockObject.SetActive(true);
            creditsUnlockObject.SetActive(false);
            trialText.text = Localisation.GetLocalisedValue("PassTrial") + " " + _base.trialToUnlockIndex;
        }
        else
        {
            trialUnlockObject.SetActive(false);
            creditsUnlockObject.SetActive(true);
            costText.text = _base.unlockCost.ToString();
        }
    }
}
