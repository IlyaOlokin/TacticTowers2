using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trial : MonoBehaviour
{
    public Sprite trialImage;
    public int index;
    [SerializeField] public string description;
    [SerializeField] public string present;
    [SerializeField] public Prise prise;
    [SerializeField] public int value;

    public enum Prise
    {
        credits,
        baseInt
    }

    public static Prise sPrise;
    public static int sValue;
    public static void GetPrise()
    {
        if (sPrise == Prise.baseInt)
        {
            var s = DataLoader.LoadString("BaseUnlocks", "10000000");
            var r = "";
            for (int j = 0; j < s.Length; j++)
            {
                if (j == sValue)
                    r += "1";
                else
                    r += s[j];
            }
            DataLoader.SaveString("BaseUnlocks", r);
        }
        else
        {
            Credits.AddCredits(sValue);
        }
    }

    public void InitPrise()
    {
        sPrise = prise;
        sValue = value;
    }
}
