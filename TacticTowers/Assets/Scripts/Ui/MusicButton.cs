using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private Image buttonSprite;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteDeactive;
    
    public void Switch()
    {
        var currentMusic = AudioManager.Instance.CurrentMusic;
        
        if (currentMusic.source.isPlaying)
        {
            DataLoader.SaveInt("isMusicOn", 0);
            AudioManager.Instance.StopMusic();
        }
        else
        {
            DataLoader.SaveInt("isMusicOn", 1);
            AudioManager.Instance.PlayMusic();
        }
        
        buttonSprite.sprite = buttonSprite.sprite == spriteActive ? spriteDeactive : spriteActive;
    }

    public void Init()
    {
        var isMusicOn = Convert.ToBoolean(DataLoader.LoadInt("isMusicOn", 1));
        buttonSprite.sprite = isMusicOn ? spriteActive : spriteDeactive;
    }
}
