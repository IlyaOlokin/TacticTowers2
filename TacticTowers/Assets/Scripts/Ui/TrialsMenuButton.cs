using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialsMenuButton : MonoBehaviour
{
    [SerializeField] private Sprite UnlockedTrials;
    [SerializeField] private Sprite BlockedTrials;
    [SerializeField] private ToolTip toolTip;
    public static bool isTrialsLocked;

   
    public void Init()
    {
        if (isTrialsLocked)
        {
            GetComponent<Image>().sprite = BlockedTrials;
            GetComponent<Button>().enabled = false;
            toolTip.enabled = true;
        }
        else
        {
            GetComponent<Image>().sprite = UnlockedTrials;
            GetComponent<Button>().enabled = true;
            toolTip.enabled = false;
        }
    }
}
