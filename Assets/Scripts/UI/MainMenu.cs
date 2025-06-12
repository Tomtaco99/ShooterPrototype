using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    
    void Awake()
    {
        AudioManager.AudioInstance.LoadVolumes();
    }

    public void Play()
    {
        SceneManager.LoadScene("Loading");
        var async = LoadAsync.LoadInstance;
        if (async != null) async.enabled = true;
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Exit()
    {
        AudioManager.AudioInstance.SaveVolumes();
        Application.Quit();
    }

    public void MasterSliderChange()
    {
        AudioManager.AudioInstance.SetVolume(SliderToFloat(masterSlider.value), "MasterVolume");
    }
    
    public void MusicSliderChange()
    {
        AudioManager.AudioInstance.SetVolume(SliderToFloat(musicSlider.value), "MusicVolume");
    }
    
    public void EffectsSliderChange()
    {
        AudioManager.AudioInstance.SetVolume(SliderToFloat(sfxSlider.value), "EffectsVolume");
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        AudioManager.AudioInstance.SaveVolumes();
    }

    private float SliderToFloat(float value)
    {
        return value * 80 - 40;
    }
}
