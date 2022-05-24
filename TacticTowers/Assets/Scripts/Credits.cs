using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Credits
{
    public static int credits;


    public static void TakeCredits(int credits)
    {
        Credits.credits -= credits;
        PlayerPrefs.SetString("Credits", Credits.credits.ToString());
    }
}
