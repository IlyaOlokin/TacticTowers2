using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> towers;

    public void OnButtonNext(int panelNum)
    {
        gameObject.SetActive(false);
        
        if (panelNum == 1)
            Time.timeScale = 3;
        else
            Time.timeScale = 1;

        if (panelNum > 2)
            foreach (var tower in towers)
                tower.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void OnButtonPlay()
    {
        SceneManager.LoadScene("GameField");
        PlayerPrefs.SetInt("isTutorialCompleted", 1);
    }
    
    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            Time.timeScale = 0;
            foreach (var tower in towers)
                tower.GetComponent<CircleCollider2D>().enabled = false;
        }
            
    }
}
