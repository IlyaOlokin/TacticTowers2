using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject musicButton;
    [SerializeField] private GameObject playButton;
    
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
        var isTutorialCompleted = Convert.ToBoolean(DataLoader.LoadInt("isTutorialCompleted", 0));
        //AudioManager.Instance.Play("ButtonClick2");
        playButton.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(isTutorialCompleted ? "BaseChooseMenu" : "Tutorial");
    }
    
    public void OnButtonUpgrades()
    {
        AudioManager.Instance.Play("ButtonClick1");
        SceneManager.LoadScene("TechsMenu");
    }

    public void OnButtonChangeLanguage(int language)
    {
        AudioManager.Instance.Play("ButtonClick1");
        Localisation.CurrentLanguage = (Language)language;
        DataLoader.SaveInt("currentLanguage", language);
        Localisation.OnLanguageChanged.Invoke();
    }

    public void OnButtonTutorial()
    {
        AudioManager.Instance.Play("ButtonClick2");
        SceneManager.LoadScene("Tutorial");
    }
}
