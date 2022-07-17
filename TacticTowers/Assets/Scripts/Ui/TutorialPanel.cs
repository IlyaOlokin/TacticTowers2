using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> towers;
    [SerializeField] private GameObject skipPanel;
    private float prevTimescale;
    
    public void OnButtonNext()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.Play("ButtonClick1");
        TutorialPanelManager.CurrentPanel++;
        Time.timeScale = prevTimescale;
        
        if (TutorialPanelManager.CurrentPanel > 6)
            foreach (var tower in towers)
                tower.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void OnButtonPlay()
    {
        PlayerPrefs.SetInt("isTutorialCompleted", 1);
        AudioManager.Instance.Play("ButtonClick2");
        SceneManager.LoadScene("GameField");
    }

    public void OnButtonSkip()
    {
        skipPanel.SetActive(true);
        AudioManager.Instance.Play("ButtonClick2");
    }
    
    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (Time.timeScale != 0)
                prevTimescale = Time.timeScale;
            
            Time.timeScale = 0;
            foreach (var tower in towers)
                tower.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
