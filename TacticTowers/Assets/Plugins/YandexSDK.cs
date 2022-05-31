using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSDK : MonoBehaviour
{
    // Создание SINGLETON
    private static YandexSDK _instance;
    public static YandexSDK Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<YandexSDK>();
            
            return _instance;
        }
    }
    private void Awake()
    {
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
    
    public event Action AuthSuccess;    //События
    public event Action DataGet;    //События
    public event Action RewardGet;  //События


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
        ShowCommonADV();
    }

    public void ShowRewardedAdvertisment()
    {
        ShowRewardADV();
        RewardGet?.Invoke();
    }
    
    public void ResetSubscriptions() => RewardGet = null; 
}





