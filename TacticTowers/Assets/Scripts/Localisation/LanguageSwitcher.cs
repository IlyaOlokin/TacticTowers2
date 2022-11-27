using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    public void ChangeLanguage(int lang)
    {
        Localisation.CurrentLanguage = (Language)lang;
        Localisation.OnLanguageChanged.Invoke();
        DataLoader.SaveInt("currentLanguage", lang);
    }

    public void SwitchLanguage()
    {
        Localisation.CurrentLanguage = Localisation.CurrentLanguage == Language.Russian ? Language.English : Language.Russian;
        Localisation.OnLanguageChanged.Invoke();
        DataLoader.SaveInt("currentLanguage", (int)Localisation.CurrentLanguage);
    }
}
