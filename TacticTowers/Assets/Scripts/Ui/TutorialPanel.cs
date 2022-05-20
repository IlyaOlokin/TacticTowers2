using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> towers;

    public void OnButtonNext(int panelNum)
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        
        if (panelNum > 2)
            foreach (var tower in towers)
                tower.GetComponent<CircleCollider2D>().enabled = true;
    }
    
    private void Start()
    {
        
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
