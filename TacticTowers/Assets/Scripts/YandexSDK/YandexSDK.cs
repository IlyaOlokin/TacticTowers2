using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using Newtonsoft.Json;
using UnityEngine;

using UnityEngine.UI;
using Timer = System.Threading.Timer;

public class YandexSDK : MonoBehaviour
{
    // Создание SINGLETON
    public static YandexSDK Instance;
    
    private float adTimer = 0;
    private float adCodldown = 300f;
    private bool adAvailable;

    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        adTimer = adCodldown;
        DontDestroyOnLoad(gameObject);
        
        Instance.Authenticate();
    }

    //
    
    [DllImport("__Internal")]
    private static extern void Auth();    // Авторизация - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void ShowCommonADV();    // Показ обычной рекламы - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void GetData();    // Получение данных - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void SetData(string data);    // Отправка данных - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void ShowRewardADV();    // Показ рекламы с наградой - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    private static extern void SetLeaderBoard(int score);
    
    public event Action AuthSuccess;    //События
    public event Action DataGet;    //События
    public event Action RewardGet;  //События

    void Update()
    {
        adTimer -= Time.fixedDeltaTime;
        adAvailable = adTimer <= 0;
    }


    public void Authenticate()    //    Авторизация
    {
#if  !UNITY_EDITOR && UNITY_WEBGL
        Auth();
#endif
    }

    public void GettingData()    // Получение данных
    {
#if  !UNITY_EDITOR && UNITY_WEBGL
        GetData();
#endif
    }

    public string SettingData()    // Сохранение данных
    {
        string jsonString = JsonConvert.SerializeObject(playerData);
#if !UNITY_EDITOR && UNITY_WEBGL
        SetData(jsonString);
#endif
        return jsonString;
    }

    public void DataGetting(string data)
    {
        playerData = JsonConvert.DeserializeObject<PlayerData>(data);
        DataLoader.LoadStartData();
    }

    public void ShowCommonAdvertisment()    // Показ обычной рекламы
    {
        if (!adAvailable) return;
        adTimer = adCodldown;
        
#if  !UNITY_EDITOR && UNITY_WEBGL
        ShowCommonADV();
#endif
    }

    public void ShowRewardedAdvertisment()
    {
#if  !UNITY_EDITOR && UNITY_WEBGL
        ShowRewardADV();
#endif
    }

    public void RewardGetting()
    {
        RewardGet?.Invoke();
    }
    
    public void RewardClose()
    {
        //RewardGet?.Invoke();
    }

    public void SetLeaderScore(int score)
    {
#if  !UNITY_EDITOR && UNITY_WEBGL
        SetLeaderBoard(score);
#endif
    }
    
    public void ResetSubscriptions() => RewardGet = null; 
}

[Serializable]
public class PlayerData
{
    public Dictionary<string, string> stringData = new Dictionary<string, string>();
    public Dictionary<string, int> intData = new Dictionary<string, int>();
}





