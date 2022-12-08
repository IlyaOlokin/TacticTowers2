using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextLocaliser : MonoBehaviour
{
    [SerializeField] private Text textField;
    [SerializeField] private string key;

    public void SetKey(string newKey)
    {
        key = newKey;
        SetText();
    }

    private void Start()
    {
        SetText();
        Localisation.OnLanguageChanged.AddListener(SetText);
    }

    private void SetText()
    {
        textField.text = Localisation.GetLocalisedValue(key);
    }
}
