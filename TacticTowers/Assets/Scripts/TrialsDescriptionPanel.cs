using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialsDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Text description;
    [SerializeField] private Text present;
    [SerializeField] private Image trialImage;
    [SerializeField] private Trial defaultTrial;

    private void Start()
    {
        GetTrialInfo(TrialsSelectManager.SelectedTrial == null
            ? defaultTrial
            : TrialsSelectManager.SelectedTrial.GetComponent<Trial>());
    }

    public void GetTrialInfo(Trial _trial)
    {
        var s = DataLoader.LoadString("TrialCompleted", "00000000");
        var trialsCompleted = new List<bool>();
        for (var i = 0; i < s.Length; i++)
        {
            char _char = s[i];
            trialsCompleted.Add(_char == '1');
        }
        description.GetComponent<TextLocaliser>().SetKey(_trial.description);
        if ( s[_trial.index] == '1')
            present.GetComponent<TextLocaliser>().SetKey("CompletedTrial");
        else
            present.GetComponent<TextLocaliser>().SetKey(_trial.present);
        trialImage.sprite = _trial.trialImage;
    }
}
