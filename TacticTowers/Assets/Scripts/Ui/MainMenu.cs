using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnButtonMusic()
    {
        
    }

    public void OnButtonSound()
    {
        
    }

    public void OnButtonPlay()
    {
        var isTutorialCompleted = Convert.ToBoolean(PlayerPrefs.GetInt("isTutorialCompleted", 0));

        SceneManager.LoadScene(isTutorialCompleted ? "GameField" : "Tutorial");
    }
    
    public void OnButtonUpgrades()
    {
        SceneManager.LoadScene("TechsMenu");
    }
}
