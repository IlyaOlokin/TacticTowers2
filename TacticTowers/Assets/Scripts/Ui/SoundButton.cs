using  System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Image buttonSprite;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteDeactive;

    public void Switch()
    {
        var audioManager = FindObjectOfType<AudioManager>();

        if (audioManager.soundEnabled)
        {
            DataLoader.SaveInt("isSoundOn", 0);
            audioManager.SoundOff();
        }
        else
        {
            DataLoader.SaveInt("isSoundOn", 1);
            audioManager.SoundOn();
        }
        
        buttonSprite.sprite = buttonSprite.sprite == spriteActive ? spriteDeactive : spriteActive;
    }

    private void Start()
    {
        var isMusicOn = Convert.ToBoolean(DataLoader.LoadInt("isSoundOn", 1));
        var audioManager = FindObjectOfType<AudioManager>();
        
        buttonSprite.sprite = isMusicOn ? spriteActive : spriteDeactive;
        if (isMusicOn)
            audioManager.SoundOn();
        else
            audioManager.SoundOff();

    }
}
