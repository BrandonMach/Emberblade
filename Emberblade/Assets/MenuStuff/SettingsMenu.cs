using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour //Detta �r skrivet av: Axel
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
            string option = resolutions[i].width + " x " + resolutions[i].height + " @" + resolutions[i].refreshRate + "Hz";            //Denna metoden loopar igenom alla tillg�ngliga sk�rmuppl�sningar
            options.Add(option);                                                                                                        //och l�gger till dem i en lista.

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)      //Denna if-satsen �r till f�r att g�ra spelarens nuvarande uppl�sning
            {                                                                                                                           //p� sk�rmen till default-value.
                currentResolutionIndex = i; 
            }
        }

        resolutionDropdown.AddOptions(options);                                                                                         //H�r l�ggs alla alternativ till i en Dropdownbox i spelet.
        resolutionDropdown.value = currentResolutionIndex;                                                                              //Detta �r det som v�ljer vilken uppl�sning som ska vara default.
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)                                                                                     //H�rifr�n och ned�t �r bara metoder som knappar och andra saker anv�nder i spelet.
    {                                                                                                                                   //Saker som att �ndra volymen, eller byta mellan fullsk�rmsl�ge.
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
