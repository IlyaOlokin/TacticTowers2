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

    public enum Prise
    {
        credits,
        baseInt
    }

    static int credits;
    static int caseInt;

    public static void GetPrise(Prise prise, int value)
    {
        if (prise == Prise.baseInt)
        {
            var s = DataLoader.LoadString("BaseUnlocks", "10000000");
            var r = "";
            for (int j = 0; j < s.Length; j++)
            {
                if (j == value)
                    r += "1";
                else
                    r += s[j];
            }
            DataLoader.SaveString("BaseUnlocks", r);
        }
        else
        {
            Credits.AddCredits(value);
        }
    }
}
