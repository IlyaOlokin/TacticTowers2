using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        throw new NotImplementedException();
    }

    public void OnButtonMusic()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick1");

    }

    public void OnButtonSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick1");

    }

    public void OnButtonPlay()
    {
        var isTutorialCompleted = Convert.ToBoolean(PlayerPrefs.GetInt("isTutorialCompleted", 0));
        FindObjectOfType<AudioManager>().Play("ButtonClick2");

        SceneManager.LoadScene(isTutorialCompleted ? "GameField" : "Tutorial");
    }
    
    public void OnButtonUpgrades()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick1");
        SceneManager.LoadScene("TechsMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Credits.AddCredits(100);
        }
    }
}
