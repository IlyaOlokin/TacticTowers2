using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    private GameObject currentPanel;
    [SerializeField] private List<GameObject> adButtons;
    [SerializeField] private List<GameObject> towers;
    
    [SerializeField] private GameObject enemies;
    [SerializeField] private Text waveText;
    [SerializeField] private Base _base;
    
    [SerializeField] private Text creditsCount;

    private bool isSessionEdnded;
    private bool wasMusicStopped;

    void Start()
    {
        Credits.LoseSessionCredits();
        YandexSDK.Instance.ResetSubscriptions();
        YandexSDK.Instance.RewardGet += OnButtonRewardedAd;
    }
    public void OnButtonRestart()
    {
        AudioManager.Instance.Play("ButtonClick1");
        ShowCommonAd();
        Resume();
        ResumeMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonMenu()
    {
        AudioManager.Instance.Play("ButtonClick1");
        ShowCommonAd();
        Resume();
        
        ResumeMusic();
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnButtonTechs()
    {
        AudioManager.Instance.Play("ButtonClick2");
        ShowCommonAd();
        Resume();
        ResumeMusic();
        SceneManager.LoadScene("TechsMenu");
    }

    private void OnButtonRewardedAd()
    {
        Credits.AcceptSessionCredits();
        TechButtonHighlight.TryHighlight();
        
        
        var audioManager = FindObjectOfType<AudioManager>();
        var music = Array.Find(audioManager.Sounds, sound => sound.name == "MainTheme");

        if (music.source.isPlaying)
        {
            audioManager.Stop("MainTheme");
            wasMusicStopped = true;
        }
        
        FillTexts(currentPanel, true);
        foreach (var button in adButtons)
        {
            button.SetActive(false);
        }
    }

    private void ResumeMusic()
    {
        if (wasMusicStopped) AudioManager.Instance.Play("MainTheme");
    }
    

    private void Update()
    {
        if (isSessionEdnded) return;
        if (_base.GetHp() <= 0)
        {
            currentPanel = defeatPanel;
            adButtons[0].SetActive(true);
            FillTexts(currentPanel, false);
            currentPanel.SetActive(true);
            Pause();
            Credits.AcceptSessionCredits();
            isSessionEdnded = true;
        }
        
        if (enemies.transform.childCount == 0)
        {
            var waveCount = waveText.text.Split('/').Select(int.Parse).ToArray();

            if (waveCount[0] == waveCount[1])
            {
                currentPanel = victoryPanel;
                adButtons[1].SetActive(true);
                FillTexts(currentPanel, false);
                currentPanel.SetActive(true);
                Pause();
                Credits.AcceptSessionCredits();
                isSessionEdnded = true;
            }
        }

    }

    private void FillTexts(GameObject panel, bool isCreditsDoubled)
    {
        panel.transform.Find("WaveCount").transform.Find("Count").GetComponent<Text>().text = waveText.text;
        if (isCreditsDoubled)
            panel.transform.Find("CreditsCount").transform.Find("Count").GetComponent<Text>().text = (Credits.creditsDuringSession * 2).ToString();
        else
            panel.transform.Find("CreditsCount").transform.Find("Count").GetComponent<Text>().text = Credits.creditsDuringSession.ToString();
        
    }
    
    private void Pause()
    {
        Time.timeScale = 0;
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = false;
    }
    
    private void Resume()
    {
        //victoryPanel.SetActive(false);
        //defeatPanel.SetActive(false);
        Time.timeScale = 1;
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = true;
    }
    
    private void ShowCommonAd()
    {
        YandexSDK SDK = FindObjectOfType<YandexSDK>();
        try
        {
            SDK.ShowCommonAdvertisment();
        }
        catch (Exception e)
        {
            Console.WriteLine("add");
        }
    }
}
