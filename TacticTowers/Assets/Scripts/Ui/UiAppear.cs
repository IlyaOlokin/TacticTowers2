using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAppear : MonoBehaviour
{
    [SerializeField] private static List<GameObject> uisStatic;
    [SerializeField] private  List<GameObject> uis;

    private void Start()
    {
        uisStatic = uis;
        
        //if (AudioManager.Instance.CurrentMusic.name != "GameField") 
        //    AudioManager.Instance.StopMusic();
        
        AudioManager.Instance.ChangeMusic("GameField");
        
        if (Convert.ToBoolean(DataLoader.LoadInt("isMusicOn", 1)))
            AudioManager.Instance.PlayMusic();
    }

    public static bool IsAnyUIActive()
    {
        foreach (var ui in uisStatic)
            if (ui.activeInHierarchy)
                return true;
        
        return false;
    }
}
