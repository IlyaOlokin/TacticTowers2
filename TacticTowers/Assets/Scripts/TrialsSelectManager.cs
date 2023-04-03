using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrialsSelectManager : MonoBehaviour
{
    public static GameObject SelectedTrial;
    public static int SelectedTrialIndex;

    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private List<GameObject> trials;

    [SerializeField] private SelectIndicator selectIndicator;
    [SerializeField] private TrialsDescriptionPanel trialDescription;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject indicatorButton;

    private List<bool> trialsCompleted;

    void Awake()
    {
        LoadCompletedTrials();
        InitializeButtons();
        SelectTrial(SelectedTrialIndex);
    }

    private void LoadCompletedTrials()
    {
        var s = DataLoader.LoadString("TrialCompleted", "00000000");
        trialsCompleted = new List<bool>();
        for (var i = 0; i < s.Length; i++)
        {
            char _char = s[i];
            trialsCompleted.Add(_char == '1');
        }
    }
    private void InitializeButtons()
    {
        for (int i = 0; i < Math.Min(buttons.Count, trials.Count); i++)
        {
            var button = buttons[i].GetComponent<TrialsSelectButton>();
            button.index = i;
            button.TrialsSelectManager = this;
            button.trialSprite = trials[i].GetComponent<Trial>().trialImage;
            button.completedGameObject.SetActive(trialsCompleted[i]);
        }
    }

    public void SelectTrial(int index)
    {
        SelectedTrial = trials[index];
        SelectedTrialIndex = index;
        DataLoader.SaveInt("selectedTrialsIndex", SelectedTrialIndex);
        trialDescription.GetTrialInfo(trials[index].GetComponent<Trial>());
        indicatorButton.SetActive(false);
        selectIndicator.GetNewDestination(buttons[index].transform.position);
        indicatorButton.SetActive(true);
    }

    public void OnPlay()
    {
        DataLoader.SaveString("PlaySceneLoad", "Trial" + (DataLoader.LoadInt("selectedTrialsIndex", 0) + 1).ToString());
        SceneManager.LoadScene("BaseChooseMenu");
    }
}
