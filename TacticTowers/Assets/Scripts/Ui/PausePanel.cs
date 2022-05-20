using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    //[SerializeField] private GameObject soundCheckMark;
    //[SerializeField] private Text levelNumberText;
    [SerializeField] private List<GameObject> towers;
    
    public void OnButtonRestart()
    {
        Resume();
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonMenu()
    {
        Resume();
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("MainMenu");
    }

    public void OnButtonClose()
    {
        Resume();
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonSound()
    {
        //soundCheckMark.SetActive(!soundCheckMark.activeInHierarchy);
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void OnButtonMusic()
    {
        
    }
    
    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = false;
        
        //var pi = FindObjectOfType<PlayerInput>();
        //if (pi != null) pi.enabled = false;
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = true;
        
        //var pi = FindObjectOfType<PlayerInput>();
        //if (pi != null) pi.enabled = true;
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
            else if (Time.timeScale != 0)
            {
                Pause();
            }
        }
    }
}
