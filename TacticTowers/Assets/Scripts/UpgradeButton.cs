using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Text upgradeLabel;
    public Text upgradeText;
    public Image upgradeImage;
    [SerializeField] private GameObject particleEffect;
    private bool isSuperCard;
    private Material material;
    private Image image;
    [SerializeField] private Material superMaterial;
    private static readonly int unscaledTime = Shader.PropertyToID("_UnscaledTime");
    private static readonly int sprite = Shader.PropertyToID("_MainTexture");


    private void OnEnable()
    {
        image = GetComponent<Image>();
        material = image.material;
    }

    private void Update()
    {
        if (!superMaterial) return;
        material.SetFloat(unscaledTime, Time.unscaledTime);
    }

    public void ChangeSprite(Sprite newSprite)
    {
        if (isSuperCard)
            material.SetTexture(sprite, newSprite.texture);
        else
            image.sprite = newSprite;
    }

    public void ActivateSuperCardEffects(bool isSuperCard)
    {
        this.isSuperCard = isSuperCard;
        
        particleEffect.SetActive(isSuperCard);
        
        if (!this.isSuperCard)
        {
            image.material = null;
            return;
        }
        image.material = superMaterial;
        material = image.material;
        image.sprite = null;
    }
}
