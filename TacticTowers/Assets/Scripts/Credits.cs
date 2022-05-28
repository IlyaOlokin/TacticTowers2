using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Credits
{
    public static int credits;
    public static int creditsDuringSession;


    public static void TakeCredits(int credits)
    {
        Credits.credits -= credits;
        PlayerPrefs.SetString("Credits", Credits.credits.ToString());
    }
    public static void AddCredits(int credits)
    {
        Credits.credits += credits;
        PlayerPrefs.SetString("Credits", Credits.credits.ToString());
    }

    public static void AddSessionCredits(int credits)
    {
        creditsDuringSession += credits;
    }

    public static void AcceptSessionCredits()
    {
        AddCredits(creditsDuringSession);
    }
    
    public static void LoseSessionCredits()
    {
        creditsDuringSession = 0;
    }
}
