using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialsMenuButton : MonoBehaviour
{
    [SerializeField] private Sprite UnlockedTrials;
    [SerializeField] private Sprite BlockedTrials;
    public static bool isTrialsUnlocked;

   
    public void Init()
    {
        if (isTrialsUnlocked)
        {
            GetComponent<Image>().sprite = BlockedTrials;
            GetComponent<Button>().enabled = false;
        }
        else
        {
            GetComponent<Image>().sprite = UnlockedTrials;
            GetComponent<Button>().enabled = true;
        }
    }
}
