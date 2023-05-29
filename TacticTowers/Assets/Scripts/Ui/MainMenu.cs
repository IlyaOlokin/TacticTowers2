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
    [SerializeField] private List<GameObject> languageButtons;
    [SerializeField] private SelectIndicator languageSelectIndicator;
    
    
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
        AudioManager.Instance.Play("ButtonClick2");
        DataLoader.SaveString("PlaySceneLoad", isTutorialCompleted ? "GameField" : "Tutorial");
        //playButton.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(isTutorialCompleted ? "BaseChooseMenu" : "LoadScene");
    }
    
    public void OnButtonUpgrades()
    {
        AudioManager.Instance.Play("ButtonClick1");
        SceneManager.LoadScene("TechsMenu");
    }

    public void OnButtonTrials()
    {
        AudioManager.Instance.Play("ButtonClick1");
        SceneManager.LoadScene("TrialsMenu");
    }

    public void InitializeLanguage()
    {
        var language = DataLoader.LoadInt("currentLanguage", 0);
        Localisation.CurrentLanguage = (Language)language;
        Localisation.OnLanguageChanged.Invoke();
        
        languageSelectIndicator.GetNewDestination(languageButtons[language].transform.position);
    }

    public void OnButtonChangeLanguage(int language)
    {
        AudioManager.Instance.Play("ButtonClick1");
        Localisation.CurrentLanguage = (Language)language;
        DataLoader.SaveInt("currentLanguage", language);
        Localisation.OnLanguageChanged.Invoke();
        languageSelectIndicator.GetNewDestination(languageButtons[language].transform.position);
    }

    public void OnButtonTutorial()
    {
        AudioManager.Instance.Play("ButtonClick2");
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
