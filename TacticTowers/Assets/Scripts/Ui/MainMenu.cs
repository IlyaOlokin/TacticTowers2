using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject musicButton;
    
    public void OnButtonMusic()
    {
        AudioManager.Instance.Play("ButtonClick1");
        musicButton.GetComponent<MusicButton>().Switch();
    }

    public void OnButtonSound()
    {
        AudioManager.Instance.Play("ButtonClick1");
        soundButton.GetComponent<SoundButton>().Switch();
    }

    public void OnButtonPlay()
    {
        var isTutorialCompleted = Convert.ToBoolean(PlayerPrefs.GetInt("isTutorialCompleted", 0));
        AudioManager.Instance.Play("ButtonClick2");

        SceneManager.LoadScene(isTutorialCompleted ? "GameField" : "Tutorial");
    }
    
    public void OnButtonUpgrades()
    {
        AudioManager.Instance.Play("ButtonClick1");
        SceneManager.LoadScene("TechsMenu");
    }
}
