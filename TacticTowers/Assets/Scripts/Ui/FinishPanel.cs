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
    public Base _base;
    
    [SerializeField] private Text creditsCount;
    private float savedTimeScale;
    private bool isSessionEnded;
    private bool wasResurrectionUsed;
    private bool wasMusicStopped;

    [Header("Resurrection Panel")] 
    [SerializeField] private GameObject basePrefab;
    private Vector3 baseTransform;
    
    [SerializeField] private Image circleTimer;
    [SerializeField] private Text textTimer;
    [SerializeField] private float timeToReact;
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
        Resume(false);
        ResumeMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonMenu()
    {
        AudioManager.Instance.Play("ButtonClick1");
        Resume(false);
        
        ResumeMusic();
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnButtonTechs()
    {
        AudioManager.Instance.Play("ButtonClick2");
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

    public void PauseMusik()
    {
        var audioManager = FindObjectOfType<AudioManager>();
        var music = Array.Find(audioManager.Sounds, sound => sound.name == "MainTheme");

        if (music.source.isPlaying)
        {
            audioManager.Stop("MainTheme");
            wasMusicStopped = true;
        }
    }

    private void ResumeMusic()
    {
        if (wasMusicStopped) AudioManager.Instance.Play("MainTheme");
    }
    
    private void Update()
    {
        if (isSessionEnded) return;
        
        if (_base.GetHp() <= 0)
        {
            Pause();
            ShowDefeatPanel();
        }
        
        if (enemies.transform.childCount == 0)
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
        currentPanel = defeatPanel;
        wasResurrectionUsed = true;
        adButtons[0].SetActive(true);
        //adButtons[1].GetComponent<Button>().onClick.AddListener(PauseMusik);
        FillTexts(currentPanel, false);
        currentPanel.SetActive(true);
        Credits.AcceptSessionCredits();
        isSessionEnded = true;
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
        TimeManager.Pause();
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = false;
    }
    
    private void Resume(bool savePreviousTimeScale)
    {
        //victoryPanel.SetActive(false);
        //defeatPanel.SetActive(false);
        if (savePreviousTimeScale)
            TimeManager.Resume();
        else
            TimeManager.SetTimeScale(1f);
        
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = true;
    }
}
