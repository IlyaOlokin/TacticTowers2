using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Language
{
    Russian,
    English
}

public static class Localisation
{
    public static Language CurrentLanguage;// = Language.Russian;

    private static readonly Dictionary<string, string> localRu = LocaleLoader.GetLocaleDictionary(Language.Russian);
    private static readonly Dictionary<string, string> localEn = LocaleLoader.GetLocaleDictionary(Language.English);

    public static string GetLocalisedValue(string key)
    {
        var value = key;
        
        switch (CurrentLanguage)
        {
            case Language.Russian:
                localRu.TryGetValue(key, out value);
                break;
            case Language.English:
                localEn.TryGetValue(key, out value);
                break;
        }

        return value;
    }

    public static readonly UnityEvent OnLanguageChanged = new UnityEvent();
}
