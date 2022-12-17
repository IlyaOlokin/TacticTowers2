using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Image buttonSprite;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteDeactive;
    [SerializeField] private AudioMixer audioMixer;
    //private bool isSoundOn;
    
    public void Switch()
    {
        // audioManager = FindObjectOfType<AudioManager>();
        //Math.Abs(value - 0.0f) < float.Epsilon
        audioMixer.GetFloat("SoundVol", out var value);
        if (Math.Abs(value - 0.0f) < float.Epsilon)
        {
            DataLoader.SaveInt("isSoundOn", 0);
            audioMixer.SetFloat("SoundVol", -80.0f);
        }
        else
        {
            DataLoader.SaveInt("isSoundOn", 1);
            audioMixer.SetFloat("SoundVol", 0.0f);
        }
        
        buttonSprite.sprite = buttonSprite.sprite == spriteActive ? spriteDeactive : spriteActive;
    }

    private void Start()
    {
        var isSoundOn = Convert.ToBoolean(DataLoader.LoadInt("isSoundOn", 1));
        buttonSprite.sprite = isSoundOn ? spriteActive : spriteDeactive;

        audioMixer.SetFloat("SoundVol", isSoundOn ? 0.0f : -80.0f);
    }
}
