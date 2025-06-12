using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance { get; private set; }
    private AudioSource _musicSource;
    [SerializeField]
    private AudioMixer _audioMixer;
    private void Awake()
    {
        if (AudioInstance == null)
        {
            AudioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _musicSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume, string target)
    {
        _audioMixer.SetFloat(target, volume);
    }

    public void ToggleAudio(AudioSource audio)
    {
        audio.mute = !audio.mute;
    }

    public void SaveVolumes()
    {
        float tempValue;
        if(_audioMixer.GetFloat("MusicVolume", out tempValue))
            PlayerPrefs.SetFloat("MusicVolume", tempValue);
        if(_audioMixer.GetFloat("EffectsVolume", out tempValue))
            PlayerPrefs.SetFloat("EffectsVolume", tempValue);
        if(_audioMixer.GetFloat("MasterVolume", out tempValue))
            PlayerPrefs.SetFloat("MasterVolume", tempValue);
        PlayerPrefs.Save();
    }

    public void LoadVolumes()
    {
        _audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        _audioMixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("MusicVolume"));
        _audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MusicVolume"));

        PlayerPrefs.Save();
    }
}
