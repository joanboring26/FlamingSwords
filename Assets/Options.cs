using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{

    public AudioMixer auMixer;
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDrop;
    public Slider mVol;
    private float startingVol;
    public Slider eVol;


    private void Start()
    {

        resolutions = Screen.resolutions;

        resolutionDrop.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string op = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(op);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }

        }

        resolutionDrop.AddOptions(options);
        resolutionDrop.value = currentResIndex;
        resolutionDrop.RefreshShownValue();


    }

    public void SetMVolume(float musicVolume) {

        Debug.Log(musicVolume);

       

        auMixer.SetFloat("MusicVol", musicVolume);
        if (musicVolume == -30)
        {
            auMixer.SetFloat("MusicVol", -100);
        }

    }

    public void SetEVolume(float effectsVolume) {

        Debug.Log(effectsVolume);
        auMixer.SetFloat("FxVol", effectsVolume);

        if (effectsVolume == -40)
        {
            auMixer.SetFloat("FxVol", -100);
        }

    }

    public void Fullscreen(bool fullScreen) {

        Screen.fullScreen = fullScreen;

    }

    public void SetRes(int resIndex) {

        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }




}
