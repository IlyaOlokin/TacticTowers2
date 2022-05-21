using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnButtonMusic()
    {
        
    }

    public void OnButtonSound()
    {
        
    }

    public void OnButtonPlay()
    {
        SceneManager.LoadScene("GameField");
    }
    
    public void OnButtonUpgrades()
    {
        SceneManager.LoadScene("UpgradesMenu");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
