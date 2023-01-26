using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSelectButton : MonoBehaviour
{
    [SerializeField] private Image baseImage;
    [NonSerialized] public Sprite baseSprite;
    public GameObject lockGameObject;
    

    [NonSerialized] public int index;
    [NonSerialized] public BaseSelectManager BaseSelectManager;

    private void Start()
    {
        baseImage.sprite = baseSprite;
    }

    public void OnSelect()
    {
        BaseSelectManager.SelectBase(index);
    }
}
