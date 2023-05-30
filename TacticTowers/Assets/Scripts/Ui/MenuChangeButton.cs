using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChangeButton : MonoBehaviour
{
    public void LoadMenu(string menuName)
    {
        AudioManager.Instance.Play("ButtonClick1");
        SceneManager.LoadScene(menuName);
    }
    
    public void OnButtonPlay()
    {
        var isTutorialCompleted = Convert.ToBoolean(DataLoader.LoadInt("isTutorialCompleted", 0));
        AudioManager.Instance.Play("ButtonClick2");
        
        DataLoader.SaveString("PlaySceneLoad", isTutorialCompleted ? "GameField" : "Tutorial");
        SceneManager.LoadScene(isTutorialCompleted ? "BaseChooseMenu" : "LoadScene");
    }

    public void LoadChooseBaseMenu()
    {
        AudioManager.Instance.Play("ButtonClick2");
        DataLoader.SaveString("PlaySceneLoad", "GameField");
        SceneManager.LoadScene("BaseChooseMenu");

    }

    public void LoadGameScene()
    {
        AudioManager.Instance.Play("ButtonClick2");
        SceneManager.LoadScene("LoadScene");
    }
}
