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
    private Sound currentMusic;
    
    public void Switch()
    {
        if (currentMusic.source.isPlaying)
        {
            DataLoader.SaveInt("isMusicOn", 0);
            AudioManager.Instance.Stop(currentMusic.name);
        }
        else
        {
            DataLoader.SaveInt("isMusicOn", 1);
            AudioManager.Instance.Play(currentMusic.name);
        }
        
        buttonSprite.sprite = buttonSprite.sprite == spriteActive ? spriteDeactive : spriteActive;
    }

    private void Start()
    {
        var isMusicOn = Convert.ToBoolean(DataLoader.LoadInt("isMusicOn", 1));

        buttonSprite.sprite = isMusicOn ? spriteActive : spriteDeactive;
        
        //currentMusic = Array.Find(AudioManager.Instance.Sounds, sound => sound.isMusic && sound.source.isPlaying);
    }
}
