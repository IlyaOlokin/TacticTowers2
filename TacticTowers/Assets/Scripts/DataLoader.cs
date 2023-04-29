using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private MusicButton musicButton;
    [SerializeField] private SoundButton soundButton;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private TrialsMenuButton trialsMenuButton;

    private static MusicButton mscButton;
    private static SoundButton sndButton;
    private static MainMenu mnMenu;
    private static TrialsMenuButton trMeButton;

    void Start()
    {
        mscButton = musicButton;
        sndButton = soundButton;
        mnMenu = mainMenu;
        trMeButton = trialsMenuButton;


        //YandexSDK.Instance.GettingData();
        LoadStartData();
    }

    public static void LoadStartData()
    {
        Technologies.LoadData();

        Credits.credits = LoadInt("Credits", 0);
        Credits.CreditsInTotal = int.Parse(LoadString("CreditsInTotal", Credits.credits.ToString()));

        Technologies.MinUpgradePrice = LoadInt("minUpgradePrice", 10);

        BaseSelectManager.SelectedBaseIndex = LoadInt("selectedBaseIndex", 0);

        Localisation.CurrentLanguage = (Language) LoadInt("currentLanguage", 0);
        Localisation.OnLanguageChanged.Invoke();

        //YandexSDK.Instance.Authenticate();
        
        AudioManager.Instance.ChangeMusic("MainTheme");
        
        if (Convert.ToBoolean(LoadInt("isMusicOn", 1)))
            AudioManager.Instance.PlayMusic();
        
        mscButton.Init();
        sndButton.Init();
        mnMenu.InitializeLanguage();

        TrialsMenuButton.isTrialsLocked = LoadInt("isTrialsUnlocked", 0) == 0;
        trMeButton.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Credits.AddCredits(1000);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public static void SaveInt(string variableName, int value)
    {
        PlayerPrefs.SetInt(variableName, Convert.ToInt16(value));
    }

    public static void SaveInt(string variableName, bool isUnlocked)
    {
        PlayerPrefs.SetInt(variableName, Convert.ToInt16(isUnlocked));
    }

    public static void SaveString(string variableName, string value)
    {
        PlayerPrefs.SetString(variableName, value);
    }

    public static string LoadString(string variableName, string defaultValue)
    {
        return PlayerPrefs.GetString(variableName, defaultValue);
    }
    
    public static int LoadInt(string variableName, int defaultValue)
    {
        return PlayerPrefs.GetInt(variableName, defaultValue);
    }
}
