using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSkipPanel : MonoBehaviour
{
    [SerializeField] private GameObject skipPanel;

    public void OnButtonSkip()
    {
        PlayerPrefs.SetInt("isTutorialCompleted", 1);
        AudioManager.Instance.Play("ButtonClick1");
        SceneManager.LoadScene("GameField");
    }

    public void OnButtonCancel()
    {
        skipPanel.SetActive(false);
        AudioManager.Instance.Play("ButtonClick1");
    }
}
