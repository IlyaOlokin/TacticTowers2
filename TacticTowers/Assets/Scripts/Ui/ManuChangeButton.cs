using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManuChangeButton : MonoBehaviour
{
    public void LoadMenu(string menuName)
    {
        SceneManager.LoadScene(menuName);
    }
}
