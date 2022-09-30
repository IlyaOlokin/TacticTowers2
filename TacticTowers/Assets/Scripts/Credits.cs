using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Credits
{
    public static int credits;
    public static int creditsDuringSession;

    public static int CreditsInTotal;


    public static void TakeCredits(int credits)
    {
        Credits.credits -= credits;
        DataLoader.SaveString("Credits", Credits.credits.ToString());

    }
    public static void AddCredits(int credits)
    {
        Credits.credits += credits;
        CreditsInTotal += credits;
        DataLoader.SaveString("Credits", Credits.credits.ToString());
        DataLoader.SaveString("CreditsInTotal", CreditsInTotal.ToString());
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
