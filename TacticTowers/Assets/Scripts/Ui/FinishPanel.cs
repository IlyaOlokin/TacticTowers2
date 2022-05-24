using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private List<GameObject> towers;
    
    [SerializeField] private GameObject enemies;
    [SerializeField] private Text waveText;
    [SerializeField] private Base _base;
    
    [SerializeField] private Text creditsCount;
    
    public void OnButtonRestart()
    {
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
        Resume();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonMenu()
    {
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
        Resume();
        
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnButtonTechs()
    {
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
        Resume();
        
        SceneManager.LoadScene("TechsMenu");
    }
    
    void Start()
    {
        
    }

    private void Update()
    {
        if (_base.GetHp() <= 0)
        {
            FillTexts(defeatPanel);
            defeatPanel.SetActive(true);

            Pause();
        }
        
        if (enemies.transform.childCount == 0)
        {
            var waveCount = waveText.text.Split('/').Select(int.Parse).ToArray();

            if (waveCount[0] == waveCount[1])
            {
                FillTexts(victoryPanel);
                victoryPanel.SetActive(true);

                Pause();
            }
        }

    }

    private void FillTexts(GameObject panel)
    {
        panel.transform.Find("WaveCount").transform.Find("Count").GetComponent<Text>().text = waveText.text;
        panel.transform.Find("CreditsCount").transform.Find("Count").GetComponent<Text>().text = creditsCount.text;
    }
    
    private void Pause()
    {
        Time.timeScale = 0;
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = false;
    }
    
    private void Resume()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
        Time.timeScale = 1;
        foreach (var tower in towers)
            tower.GetComponent<CircleCollider2D>().enabled = true;
    }
}
