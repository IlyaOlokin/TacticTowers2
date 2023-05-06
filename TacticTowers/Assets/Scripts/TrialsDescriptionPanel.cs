using UnityEngine;
using UnityEngine.UI;

public class TrialsDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Text description;
    [SerializeField] private Trial defaultTrial;
    [SerializeField] private GameObject prizeBase;
    [SerializeField] private Image baseImage;
    [SerializeField] private Text presentBaseDescription;
    [SerializeField] private GameObject prizeCredits;
    [SerializeField] private Text presentCreditsDescription;
    [SerializeField] private GameObject trialCompleted;


    private void Start()
    {
        GetTrialInfo(TrialsSelectManager.SelectedTrial == null
            ? defaultTrial
            : TrialsSelectManager.SelectedTrial.GetComponent<Trial>());
    }

    public void GetTrialInfo(Trial _trial)
    {
        trialCompleted.SetActive(false);
        prizeBase.SetActive(false);
        prizeCredits.SetActive(false);

        var s = DataLoader.LoadString("TrialCompleted", "00000000");
        description.GetComponent<TextLocaliser>().SetKey(_trial.description);
        if (s[_trial.index] == '1')
        {
            trialCompleted.SetActive(true);
        }
        else
        {
            if (_trial.GetComponent<Trial>().prise == Trial.Prise.credits)
            {
                prizeCredits.SetActive(true);
                presentCreditsDescription.text = _trial.value.ToString();
            }
            else
            {
                prizeBase.SetActive(true);
                baseImage.sprite = TrialManager.Instance.spritesBase[_trial.value];
                presentBaseDescription.GetComponent<TextLocaliser>().SetKey(_trial.present);
            }
        }
    }
}
