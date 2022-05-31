using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private Image buttonSprite;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteDeactive;

    public void Switch()
    {
        var audioManager = FindObjectOfType<AudioManager>();
        var music = Array.Find(audioManager.Sounds, sound => sound.name == "MainTheme");

        if (music.source.isPlaying)
        {
            PlayerPrefs.SetInt("isMusicOn", 0);
            audioManager.Stop("MainTheme");
        }
        else
        {
            PlayerPrefs.SetInt("isMusicOn", 1);
            audioManager.Play("MainTheme");
        }
        
        buttonSprite.sprite = buttonSprite.sprite == spriteActive ? spriteDeactive : spriteActive;
    }

    private void Start()
    {
        var isMusicOn = Convert.ToBoolean(PlayerPrefs.GetInt("isMusicOn", 1));

        buttonSprite.sprite = isMusicOn ? spriteActive : spriteDeactive;
    }
}
