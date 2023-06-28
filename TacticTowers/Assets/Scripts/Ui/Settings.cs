using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    [SerializeField] private AudioMixer audioMixer;
    public Slider soundSlider;
    public Slider musicSlider;

    Resolution[] resolutions;
    List<Resolution> resolutions169;

    public void Init()
    {
        resolutionDropdown.ClearOptions();
        var optipns = new List<string>();
        resolutions = Screen.resolutions;
        resolutions169 = new List<Resolution>();

        for (var i = 0; i < resolutions.Length; i++)
        {
            if (Math.Abs(((float)resolutions[i].width / (float)resolutions[i].height) - (16f / 9f)) < 0.0001f && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                var option = $"{resolutions[i].width}x{resolutions[i].height}";
                optipns.Add(option);
                resolutions169.Add(resolutions[i]);
            }
        }

        resolutionDropdown.AddOptions(optipns);
        resolutionDropdown.RefreshShownValue();
        
        LoadSettings(GetCurrentResolutionIndex());
        SetResolution(GetCurrentResolutionIndex());
        ChangeSoundVolume();
        ChangeMusicVolume();
    }

    public int GetCurrentResolutionIndex()
    {
        return DataLoader.LoadInt("ResolutionPreference", resolutions169.Count - 1);
    }

    public void ChangeSoundVolume()
    {
        if (soundSlider.value == 0) audioMixer.SetFloat("SoundVol", -80.0f);
        else audioMixer.SetFloat("SoundVol", -20.0f + (20.0f * soundSlider.value));
        DataLoader.SaveString("SoundVolume", soundSlider.value.ToString());
    }
    public void OpenSettings()
    {
        LoadSettings(GetCurrentResolutionIndex());
        gameObject.SetActive(true);
    }

    public void ChangeMusicVolume()
    {
        if (musicSlider.value == 0) audioMixer.SetFloat("MusicVol", -80.0f);
        else audioMixer.SetFloat("MusicVol", -20.0f + (20.0f * musicSlider.value));
        DataLoader.SaveString("MusicVolume", musicSlider.value.ToString());
    }
    public void SetResolution(int resolutionIndex)
    {
        var resolution = resolutions169[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
        DataLoader.SaveInt("ResolutionPreference", resolutionDropdown.value);
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        resolutionDropdown.value = DataLoader.LoadInt("ResolutionPreference", currentResolutionIndex);
        soundSlider.value = float.Parse(DataLoader.LoadString("SoundVolume", "1"));
        musicSlider.value = float.Parse(DataLoader.LoadString("MusicVolume", "1"));
    }
}
