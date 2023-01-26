using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private List<GameObject> towers;
    [SerializeField] private Text creditsCount;
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject musicButton;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private AudioMixer audioMixer;
    private bool isForRestart;

    public void OnButtonRestart()
    {
        isForRestart = true;
        ActivateConfirmPanel(isForRestart);
        AudioManager.Instance.Play("ButtonClick1");

    }
    
    public void OnButtonMenu()
    {
        isForRestart = false;
        ActivateConfirmPanel(isForRestart);
        AudioManager.Instance.Play("ButtonClick2");

    }

    private void ActivateConfirmPanel(bool isForRestart)
    {
        pausePanel.SetActive(false);
        confirmPanel.transform.Find("CreditsCount").transform.Find("Count").GetComponent<Text>().text = creditsCount.text;
        confirmButton.transform.Find("Text").GetComponent<TextLocaliser>().SetKey(isForRestart ? "menuRestartButton" : "menuMenuButton"); 
        confirmPanel.SetActive(true);
    }

    public void OnButtonClose()
    {
        Resume();
        AudioManager.Instance.Play("ButtonClick1");
    }

    public void OnButtonSound()
    {
        AudioManager.Instance.Play("ButtonClick1");
        soundButton.GetComponent<SoundButton>().Switch();
    }

    public void OnButtonMusic()
    {
        AudioManager.Instance.Play("ButtonClick1");
        musicButton.GetComponent<MusicButton>().Switch();
    }

    public void OnButtonCancel()
    {
        confirmPanel.SetActive(false);
        pausePanel.SetActive(true);
        AudioManager.Instance.Play("ButtonClick2");

    }

    public void OnButtonPause()
    {
        Pause();
    }

    public void OnButtonContinue()
    {
        ShowCommonAd();
        Resume();
        AudioManager.Instance.Play("ButtonClick1");
        Credits.LoseSessionCredits();
        SceneManager.LoadScene(isForRestart ? SceneManager.GetActiveScene().name : "MainMenu");
    }
    
    private void Pause()
    {
        TimeManager.Pause(audioMixer);
        pausePanel.SetActive(true);
        AudioManager.Instance.Play("ButtonClick2");
    }

    private void Resume()
    {
        TimeManager.Resume(audioMixer);
        pausePanel.SetActive(false);
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void Start()
    {
        Resume();
    }

    private void OnEnable()
    {
        musicButton.GetComponent<MusicButton>().Init();
        soundButton.GetComponent<SoundButton>().Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeInHierarchy)
            {
                Resume();
            }
            else if (!pausePanel.activeInHierarchy)
            {
                if (towers.Any(tower =>  tower.GetComponent<TowerDrag>().needToDrop)) return;
                Pause();
            }
        }
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
