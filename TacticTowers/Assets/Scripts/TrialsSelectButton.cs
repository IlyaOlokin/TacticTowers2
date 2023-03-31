using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialsSelectButton : MonoBehaviour
{
    [SerializeField] private Image trialImage;
    [NonSerialized] public Sprite trialSprite;
    public GameObject completedGameObject;

    [NonSerialized] public int index;
    [NonSerialized] public TrialsSelectManager TrialsSelectManager;

    private void Start()
    {
        trialImage.sprite = trialSprite;
    }

    public void OnSelect()
    {
        TrialsSelectManager.SelectTrial(index);
    }
}
