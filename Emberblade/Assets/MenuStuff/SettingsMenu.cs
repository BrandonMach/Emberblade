using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour //Detta är skrivet av: Axel
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @" + resolutions[i].refreshRate + "Hz";            //Denna metoden loopar igenom alla tillgängliga skärmupplösningar
            options.Add(option);                                                                                                        //och lägger till dem i en lista.

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)      //Denna if-satsen är till för att göra spelarens nuvarande upplösning
            {                                                                                                                           //på skärmen till default-value.
                currentResolutionIndex = i; 
            }
        }

        resolutionDropdown.AddOptions(options);                                                                                         //Här läggs alla alternativ till i en Dropdownbox i spelet.
        resolutionDropdown.value = currentResolutionIndex;                                                                              //Detta är det som väljer vilken upplösning som ska vara default.
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)                                                                                     //Härifrån och nedåt är bara metoder som knappar och andra saker använder i spelet.
    {                                                                                                                                   //Saker som att ändra volymen, eller byta mellan fullskärmsläge.
        Resolution resolution = resolutions[resolutionIndex];   
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);                                                   
    }                                                                                                                                       

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
