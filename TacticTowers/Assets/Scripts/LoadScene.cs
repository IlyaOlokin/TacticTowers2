using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private string sceneName;
    public Slider loadSlider;

    void Start()
    {
        sceneName = DataLoader.LoadString("PlaySceneLoad", "GameField");
        StartCoroutine(LoadNextScene());
    }


    IEnumerator LoadNextScene()
    {
        yield return null;
        AsyncOperation oper = SceneManager.LoadSceneAsync(sceneName);
        while (!oper.isDone)
        {
            var progress = oper.progress / 0.9f;
            loadSlider.value = progress;
            yield return null;
        }
    }
}
