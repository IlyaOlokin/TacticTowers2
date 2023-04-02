using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public String toolTipText;
    [SerializeField] private GameObject textWindowPrefab;
    private GameObject textWindow;

    private float mouseOverTimer;
    [SerializeField] private float showTipDelay;
    private bool mouseOver = false;
    

    private void Start()
    {
       textWindow = Instantiate(textWindowPrefab, transform.position, Quaternion.identity, transform);
       textWindow.GetComponent<ToolTipWindow>().text.text = toolTipText;
       
       HideTip();
    }

    private void ShowTip()
    {
        textWindow.SetActive(true);
        textWindow.transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void HideTip()
    {
        textWindow.SetActive(false);
    }
    
    void Update()
    {
        if (mouseOver)
        {
            mouseOverTimer += Time.unscaledDeltaTime;
            if (mouseOverTimer >= showTipDelay && !textWindow.activeSelf)
            {
                ShowTip();
            }
        }
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        mouseOverTimer = 0;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        HideTip();
    }
}
