using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        //GetComponent<Button>().onClick.AddListener(YandexSDK.Instance.ShowRewardedAdvertisment);
        GetComponent<Button>().onClick.AddListener(transform.parent.parent.GetComponent<FinishPanel>().PauseMusik);
    }
}
