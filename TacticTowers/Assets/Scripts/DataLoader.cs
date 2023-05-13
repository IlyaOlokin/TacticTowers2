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
    [SerializeField] private Settings settingsPanel;

    private static MainMenu mnMenu;
    private static TrialsMenuButton trMeButton;
    private static Settings settings;
    
    public static GameData gameData;
    
    [Header("File Storage Config")] 
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    
    private static FileDataHandler dataHandler;

    void Start()
    {
        mnMenu = mainMenu;
        trMeButton = trialsMenuButton;
        settings = settingsPanel;

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        
        LoadStartData();
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

        TrialsMenuButton.isTrialsLocked = LoadInt("isTrialsUnlocked", 0) == 0;
        trMeButton.Init();
        settings.Init();
        
    }

    private static void NewGame()
    {
        gameData = new GameData();
    }

    private static void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            NewGame();
        }
    }

    private static void SaveGame()
    {
        dataHandler.Save(gameData);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Credits.AddCredits(1000);
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
        if (!gameData.intData.ContainsKey(variableName))
            gameData.intData.Add(variableName, isUnlocked);
        else
            gameData.intData[variableName] = isUnlocked;
        
        SaveGame();
    }

    public static void SaveInt(string variableName, bool isUnlocked)
    {
        if (!gameData.intData.ContainsKey(variableName))
            gameData.intData.Add(variableName, Convert.ToInt16(isUnlocked));
        else
            gameData.intData[variableName] = Convert.ToInt16(isUnlocked);
        
        SaveGame();
    }

    public static void SaveString(string variableName, string value)
    {
        if (!gameData.stringData.ContainsKey(variableName))
            gameData.stringData.Add(variableName, value);
        else
            gameData.stringData[variableName] = value;
        
        SaveGame();
    }

    public static string LoadString(string variableName, string defaultValue)
    {
        if (!gameData.stringData.ContainsKey(variableName))
            return defaultValue;
       
        return gameData.stringData[variableName];
    }
    
    public static int LoadInt(string variableName, int defaultValue)
    {
        if (!gameData.intData.ContainsKey(variableName))
            return defaultValue;
       
        return gameData.intData[variableName];
    }
}

[Serializable]
public class GameData
{
    public SerializableDictionary<string, string> stringData = new SerializableDictionary<string, string>();
    public SerializableDictionary<string, int> intData = new SerializableDictionary<string, int>();
}
