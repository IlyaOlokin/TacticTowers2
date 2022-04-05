using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private static int money;
    private static Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = money.ToString();
        SetMoney(0);
        
    }
    public static void AddMoney(int income)
    {
        money += income;
        text.text = money.ToString();
    }
    
    public static void TakeMoney(int cost)
    {
        money -= cost;
        text.text = money.ToString();
    }

    public static int GetMoney()
    {
        return money;
    }

    private static void SetMoney(int _money)
    {
        money = _money;
        text.text = money.ToString();
    }
}
