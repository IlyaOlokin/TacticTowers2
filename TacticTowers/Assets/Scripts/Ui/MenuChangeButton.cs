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
}
