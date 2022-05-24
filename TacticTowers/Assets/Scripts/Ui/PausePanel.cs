using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private List<GameObject> towers;
    [SerializeField] private Text creditsCount;
    private bool isForRestart;
    
    public void OnButtonRestart()
    {
        pausePanel.SetActive(false);
        
        isForRestart = true;
        confirmPanel.transform.Find("CreditsCount").transform.Find("Count").GetComponent<Text>().text = creditsCount.text;
        confirmPanel.SetActive(true);
    }
    
    public void OnButtonMenu()
    {
        pausePanel.SetActive(false);

        isForRestart = false;
        confirmPanel.transform.Find("CreditsCount").transform.Find("Count").GetComponent<Text>().text = creditsCount.text;
        confirmPanel.SetActive(true);
    }

    public void OnButtonClose()
    {
        Resume();
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonSound()
    {
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonMusic()
    {
        
    }

    public void OnButtonCancel()
    {
        confirmPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void OnButtonContinue()
    {
        Resume();
        //FindObjectOfType<AudioManager>().Play("ButtonClick");

        SceneManager.LoadScene(isForRestart ? SceneManager.GetActiveScene().name : "MainMenu");
    }
    
    private void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = false;
        
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    private void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeInHierarchy)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
