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
    [SerializeField] private List<GameObject> adButtons;
    [SerializeField] private List<GameObject> towers;
    
    [SerializeField] private GameObject enemies;
    [SerializeField] private Text waveText;
    public Base _base;
    
    [SerializeField] private Text creditsCount;
    private float savedTimeScale;
    private bool isSessionEnded;
    private bool wasResurrectionUsed;
    private bool wasMusicStopped;

    [Header("Resurrection Panel")] 
    [SerializeField] private GameObject basePrefab;
    private Vector3 baseTransform;
    [SerializeField] private GameObject ResurrectionPanel;
    [SerializeField] private Image circleTimer;
    [SerializeField] private Text textTimer;
    [SerializeField] private float timeToReact;
    [SerializeField] private AudioMixer audioMixer;

    private float timer;
    private bool isRewarding;

    

    void Start()
    {
        baseTransform = _base.gameObject.transform.position;
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

    private void OnButtonRewardedAd()
    {
        Credits.AcceptSessionCredits();
        TechButtonHighlight.TryHighlight();
        
        ResumeMusic();
        
        FillTexts(currentPanel, true);
        foreach (var button in adButtons)
        {
            button.SetActive(false);
        }
    }

    public void OnButtonResurrectionAd()
    {
        isRewarding = true;
        PauseMusic();
        YandexSDK.Instance.ShowRewardedAdvertisment();
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
            if (!wasResurrectionUsed) UpdateResurrectionPanel();
            return;
        }
        if (_base.GetHp() <= 0)
        {
            Pause();
            
            if(!wasResurrectionUsed) ShowResurrectionPanel();
            else ShowDefeatPanel();
        }
        
        if (SceneManager.GetActiveScene().name != "Tutorial" && enemies.transform.childCount == 0)
        {
            var waveCount = waveText.text.Split('/').Select(int.Parse).ToArray();

            if (waveCount[0] == waveCount[1])
            {
                ShowVictoryPanel();
            }
        }
    }

    private void ShowVictoryPanel()
    {
        YandexSDK.Instance.ResetSubscriptions();
        YandexSDK.Instance.RewardGet += OnButtonRewardedAd;
        YandexSDK.Instance.RewardGet += ResumeMusic;
        currentPanel = victoryPanel;
        adButtons[1].SetActive(true);
        //adButtons[1].GetComponent<Button>().onClick.AddListener(PauseMusik);
        FillTexts(currentPanel, false);
        currentPanel.SetActive(true);
        Pause();
        Credits.AcceptSessionCredits();
        isSessionEnded = true;
    }
    
    private void ShowDefeatPanel()
    {
        YandexSDK.Instance.ResetSubscriptions();
        YandexSDK.Instance.RewardGet += OnButtonRewardedAd;
        YandexSDK.Instance.RewardGet += ResumeMusic;
        currentPanel = defeatPanel;
        wasResurrectionUsed = true;
        adButtons[0].SetActive(true);
        //adButtons[1].GetComponent<Button>().onClick.AddListener(PauseMusik);
        FillTexts(currentPanel, false);
        currentPanel.SetActive(true);
        Credits.AcceptSessionCredits();
        isSessionEnded = true;
    }

    private void UpdateResurrectionPanel()
    {
        if (isRewarding) return;
        timer -= Time.unscaledDeltaTime;
        circleTimer.fillAmount = timer / timeToReact;
        textTimer.text = Math.Ceiling(timer).ToString();
        if (timer <= 0)
        {
            ResurrectionPanel.SetActive(false);
            ShowDefeatPanel();
        }
    }
    
    private void ShowResurrectionPanel()
    {
        YandexSDK.Instance.ResetSubscriptions();
        YandexSDK.Instance.RewardGet += Resurrection;
        YandexSDK.Instance.RewardGet += ResumeMusic;
        isSessionEnded = true;
        ResurrectionPanel.SetActive(true);
        timer = timeToReact;
    }

    private void Resurrection()
    {
        isRewarding = false;
        //var newBase = Instantiate(basePrefab, baseTransform, Quaternion.identity);
        //_base = newBase.GetComponent<Base>();
        //_base.TakeDamage(_base.GetMaxHp() / 2f);
        _base.hp = _base.maxHp / 2f;
        _base.UpdateHpBar();
        ResurrectionPanel.SetActive(false);
        isSessionEnded = false;
        Resume(true);
        wasResurrectionUsed = true;
        AudioManager.Instance.Play("ButtonClick2");
        var tempEnemies = new List<Enemy>();
        foreach (var e in EnemySpawner.enemies)
        {
            tempEnemies.Add(e.GetComponent<Enemy>());
        }
        foreach (var e in tempEnemies)
        {
            e.TakeDamage(20000, DamageType.Normal, new Vector3());
        }
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
        //victoryPanel.SetActive(false);
        //defeatPanel.SetActive(false);
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
        catch 
        {
            Console.WriteLine("add");
        }
    }
}
