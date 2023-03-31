using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialsMenuButton : MonoBehaviour
{
    [SerializeField] private Sprite UnlockedTrials;
    [SerializeField] private Sprite BlockedTrials;

    void Start()
    {
        if (DataLoader.LoadInt("isTrialsUnlocked", 0) == 0)
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
