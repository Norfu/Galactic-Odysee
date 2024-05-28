using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Settings : MonoBehaviour
{
    Resolution[] resolutions;

    public TMPro.TMP_Dropdown Dropresu;

    public AudioMixer audioMixer;
    public void Start()
    {
        resolutions = Screen.resolutions;
        Dropresu.ClearOptions();
        List<string> options = new List<string>();
        int currentresolutionindex = 0;


        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentresolutionindex = i;
            }
        }
        Dropresu.AddOptions(options);
        Dropresu.value = currentresolutionindex;
        Dropresu.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume",volume);
    }

    public void setfullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void setresolution(int resolutionindex) 
    { 
        Resolution resolution = resolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
