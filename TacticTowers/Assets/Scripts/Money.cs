using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private static float money;
    private static Text text;
    private static Animation animation;

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = money.ToString();
        SetMoney(0);
        animation = GetComponent<Animation>();

    }
    public static void AddMoney(float income)
    {
        money += income * Technologies.MoneyMultiplier;
        animation.Stop("MoneyAnimation");
        WriteMoney();
        animation.Play("MoneyAnimation");
    }
    
    public static void TakeMoney(int cost)
    {
        money -= cost;
        WriteMoney();
    }

    public static float GetMoney()
    {
        return money;
    }

    private static void SetMoney(int _money)
    {
        money = _money;
        WriteMoney();

    }
    
    private static void WriteMoney()
    {
        text.text = Mathf.Floor(money).ToString();
    }

}
