using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChangeButton : MonoBehaviour
{
    public void LoadMenu(string menuName)
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick1");
        SceneManager.LoadScene(menuName);
    }
}
