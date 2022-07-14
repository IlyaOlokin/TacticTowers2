using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using UnityEngine;
using Timer = System.Threading.Timer;

public class YandexSDK : MonoBehaviour
{
    // Создание SINGLETON
    public static YandexSDK Instance;
    
    private float adTimer = 0;
    private float adCodldown = 300f;
    private bool adAvailable;
    
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
        Auth();
    }

    public void GettingData()    // Получение данных
    {
        GetData();
    }

    public void SettingData(string data)    // Сохранение данных
    {
        SetData(data);
    }

    public void ShowCommonAdvertisment()    // Показ обычной рекламы
    {
        if (!adAvailable) return;
        adTimer = adCodldown;
        ShowCommonADV();
    }

    public void ShowRewardedAdvertisment()
    {
        ShowRewardADV();
    }

    public void RewardGetting()
    {
        //RewardGet?.Invoke();
    }
    
    public void RewardClose()
    {
        RewardGet?.Invoke();
    }

    public void SetLeaderScore(int score)
    {
        SetLeaderBoard(score);
    }
    
    public void ResetSubscriptions() => RewardGet = null; 
}





