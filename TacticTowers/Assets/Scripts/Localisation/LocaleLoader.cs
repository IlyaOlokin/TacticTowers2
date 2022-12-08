using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LocaleLoader
{
    public static TextAsset localRu = Resources.Load<TextAsset>("Localisation/localRu");
    public static TextAsset localEn = Resources.Load<TextAsset>("Localisation/localEn");
    
    private static char[] lineSeparator = {'\n'};
    private static char[] fieldSeparator = {':'};
    
    public static Dictionary<string, string> GetLocaleDictionary(Language language)
    {
        var dictionary = new Dictionary<string, string>();
        var currentLocal = language switch
        {
            Language.Russian => localRu,
            Language.English => localEn,
        };
        
        var lines = currentLocal.text.Split(lineSeparator, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var fields = line.Split(fieldSeparator, StringSplitOptions.RemoveEmptyEntries);

            var key = fields[0];
            if (dictionary.ContainsKey(key))
                continue;
            
            dictionary.Add(key, fields[1]);
        }

        return dictionary;
    }
}
