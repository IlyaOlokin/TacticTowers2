using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    private GameObject currentPanel;
    [SerializeField] private List<GameObject> towers;
    
    [SerializeField] private GameObject enemies;
    [SerializeField] private Text waveText;
    public Base _base;
    
    [SerializeField] private Text creditsCount;
    private float savedTimeScale;
    private bool isSessionEnded;
    private bool wasMusicStopped;
    
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Notification notification;

    private float timer;
    private bool isRewarding;


    void Start()
    {
        Credits.LoseSessionCredits();
    }
    
    public void OnButtonRestart()
    {
        AudioManager.Instance.Play("ButtonClick1");
        ShowCommonAd();
        Resume(false);
        ResumeMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonMenu()
    {
        AudioManager.Instance.Play("ButtonClick1");
        ShowCommonAd();
        Resume(false);
        ResumeMusic();
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnButtonTechs()
    {
        AudioManager.Instance.Play("ButtonClick2");
        ShowCommonAd();
        Resume(false);
        ResumeMusic();
        SceneManager.LoadScene("TechsMenu");
    }

    public void PauseMusic()
    {
        if (Convert.ToBoolean(DataLoader.LoadInt("isMusicOn", 1)))
        {
            AudioManager.Instance.StopMusic();
            wasMusicStopped = true;
        }
    }

    private void ResumeMusic()
    {
        if (wasMusicStopped) AudioManager.Instance.PlayMusic();
    }
    
    private void Update()
    {
        if (isSessionEnded)
        {
            return;
        }
        if (_base.GetHp() <= 0)
        {
            Pause();
            
            if(!wasResurrectionUsed) ShowResurrectionPanel();
            else ShowDefeatPanel();
        }

        if (EnemySpawner.enemies.Count != 0) return;

        if (SceneManager.GetActiveScene().name == "GameField")
        {
            var waveCount = waveText.text.Split('/').Select(int.Parse).ToArray();

            if (waveCount[0] == waveCount[1])
            {
                ShowVictoryPanel();
            }
        }

        if (SceneManager.GetActiveScene().name != "Tutorial" && SceneManager.GetActiveScene().name != "GameField")
        {
            var waveCount = waveText.text.Split('/').Select(int.Parse).ToArray();

            if (waveCount[0] == waveCount[1])
            {
                ShowVictoryPanelOnTrial();
            }
        }
    }

    private void ShowVictoryPanelOnTrial()
    {
        currentPanel = victoryPanel;
        currentPanel.transform.Find("CreditsCount").gameObject.SetActive(false);
        //adButtons[1].GetComponent<Button>().onClick.AddListener(PauseMusik);
        currentPanel.SetActive(true);
        Pause();
        Credits.AcceptSessionCredits();
        isSessionEnded = true;

        var trialCompletedList = new List<char>();
        foreach (var i in DataLoader.LoadString("TrialCompleted", "00000000")) trialCompletedList.Add(i);

        var trialCompleted1 = DataLoader.LoadString("TrialCompleted", "00000000");
        var j = int.Parse(SceneManager.GetActiveScene().name.Substring(5)) - 1;
        trialCompletedList[j] = '1';
        currentPanel.transform.Find("WaveCount").transform.Find("Count").GetComponent<Text>().text = waveText.text;
        if (trialCompletedList[j] != trialCompleted1[j])
        {
            Trial.GetPrise();
            
            if (Trial.sPrise == Trial.Prise.credits)
            {
                currentPanel.transform.Find("CreditsCountTrial").transform.Find("Count").GetComponent<Text>().text = "+" + Trial.sValue;
                currentPanel.transform.Find("CreditsCountTrial").gameObject.SetActive(true);
            }
            else
            {
                currentPanel.transform.Find("BaseCount").transform.Find("BaseIndex").GetComponent<Text>().text = " ¹" + (Trial.sValue + 1);
                currentPanel.transform.Find("BaseCount").transform.Find("Image").GetComponent<Image>().sprite = TrialManager.Instance.spritesBase[Trial.sValue];
                currentPanel.transform.Find("BaseCount").gameObject.SetActive(true);
            }
        }
            

        DataLoader.SaveString("TrialCompleted", string.Join("", trialCompletedList));
    }

    private void ShowVictoryPanel()
    {
        YandexSDK.Instance.ResetSubscriptions();
        YandexSDK.Instance.RewardGet += OnButtonRewardedAd;
        YandexSDK.Instance.RewardGet += ResumeMusic;
        currentPanel = victoryPanel;
        //adButtons[1].GetComponent<Button>().onClick.AddListener(PauseMusik);
        FillTexts(currentPanel, false);
        currentPanel.SetActive(true);
        Pause();
        Credits.AcceptSessionCredits();
        isSessionEnded = true;
        UnlockTrials();
    }
    
    private void ShowDefeatPanel()
    {
        YandexSDK.Instance.ResetSubscriptions();
        YandexSDK.Instance.RewardGet += OnButtonRewardedAd;
        YandexSDK.Instance.RewardGet += ResumeMusic;
        currentPanel = defeatPanel;
        FillTexts(currentPanel, false);
        currentPanel.SetActive(true);
        Credits.AcceptSessionCredits();
        isSessionEnded = true;
        UnlockTrials();
    }

    private void UnlockTrials()
    {
        if (SceneManager.GetActiveScene().name != "GameField") return;
        if (!Convert.ToBoolean(DataLoader.LoadInt("isTrialsLocked", 0)))
            NotificationManager.Instance.GetNotification(notification);
        DataLoader.SaveInt("isTrialsLocked", 1);
    }

    private void FillTexts(GameObject panel, bool isCreditsDoubled)
    {
        panel.transform.Find("WaveCount").transform.Find("Count").GetComponent<Text>().text = waveText.text;
        if (isCreditsDoubled)
            panel.transform.Find("CreditsCount").transform.Find("Count").GetComponent<Text>().text = "+" + (Credits.creditsDuringSession * 2).ToString();
        else
            panel.transform.Find("CreditsCount").transform.Find("Count").GetComponent<Text>().text = "+" + Credits.creditsDuringSession.ToString();
        
    }
    
    private void Pause()
    {
        savedTimeScale = Time.timeScale;
        TimeManager.Pause(audioMixer);
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = false;
    }
    
    private void Resume(bool savePreviousTimeScale)
    {
        if (savePreviousTimeScale)
            TimeManager.Resume(audioMixer);
        else
            TimeManager.SetTimeScale(1f);
        
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = true;
    }
    
    private void ShowCommonAd()
    {
        try
        {
            YandexSDK.Instance.ShowCommonAdvertisment();
        }
        catch (Exception e)
        {
            Console.WriteLine("add");
        }
    }
}
