using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private TrialsMenuButton trialsMenuButton;
    [SerializeField] private Settings settingsPanel;

    private static MainMenu mnMenu;
    private static TrialsMenuButton trMeButton;
    private static Settings settings;

    [Header("File Storage Config")] 
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    void Start()
    {
        mnMenu = mainMenu;
        trMeButton = trialsMenuButton;
        settings = settingsPanel;
        
        YandexSDK.Instance.GettingData();
        //LoadStartData();
    }

    public static void LoadStartData()
    {
        LoadGame();
        
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
        
        mnMenu.InitializeLanguage();

        TrialsMenuButton.isTrialsLocked = LoadInt("isTrialsLocked", 0) == 0;
        trMeButton.Init();
        settings.Init();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Credits.AddCredits(100);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            NewGame();
            SaveGame();
            LoadStartData();
        }
    }

    public static void SaveInt(string variableName, int isUnlocked)
    {
        if (!YandexSDK.Instance.playerData.intData.ContainsKey(variableName))
            YandexSDK.Instance.playerData.intData.Add(variableName, isUnlocked);
        else
            YandexSDK.Instance.playerData.intData[variableName] = isUnlocked;
        
        YandexSDK.Instance.SettingData();
    }

    public static void SaveInt(string variableName, bool isUnlocked)
    {
        if (!YandexSDK.Instance.playerData.intData.ContainsKey(variableName))
            YandexSDK.Instance.playerData.intData.Add(variableName, Convert.ToInt16(isUnlocked));
        else
            YandexSDK.Instance.playerData.intData[variableName] = Convert.ToInt16(isUnlocked);
        YandexSDK.Instance.SettingData();
    }

    public static void SaveString(string variableName, string value)
    {
        PlayerPrefs.SetString(variableName, value);
        if (!YandexSDK.Instance.playerData.stringData.ContainsKey(variableName))
            YandexSDK.Instance.playerData.stringData.Add(variableName, value);
        else
            YandexSDK.Instance.playerData.stringData[variableName] = value;
        YandexSDK.Instance.SettingData();
    }

    public static string LoadString(string variableName, string defaultValue)
    {
        if (!YandexSDK.Instance.playerData.stringData.ContainsKey(variableName))
            return defaultValue;
       
        return YandexSDK.Instance.playerData.stringData[variableName];
    }
    
    public static int LoadInt(string variableName, int defaultValue)
    {
        if (!YandexSDK.Instance.playerData.intData.ContainsKey(variableName))
            return defaultValue;
       
        return YandexSDK.Instance.playerData.intData[variableName];
    }
}
