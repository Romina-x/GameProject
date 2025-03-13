using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Manages the game's audio mixer settings, including master, sound effects, and music volumes.
/// </summary>
public class SoundMixerManager : MonoBehaviour
{
    // Main mixer 
    [SerializeField] private AudioMixer _audioMixer;

    // UI sliders that control the volumes
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        // Load saved volumes and update sliders
        LoadVolumeSettings();
    }

    /// <summary>
    /// Sets the master volume level and saves it to PlayerPrefs.
    /// </summary>
    /// <param name="level">The volume level (expected range: 0.0001 to 1.0).</param>
    public void SetMasterVolume(float level)
    {
        _audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    /// <summary>
    /// Sets the sound effects volume level and saves it to PlayerPrefs.
    /// </summary>
    /// <param name="level">The volume level (expected range: 0.0001 to 1.0).</param>
    public void SetSoundFXVolume(float level)
    {
        _audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", level);
    }

    /// <summary>
    /// Sets the music volume level and saves it to PlayerPrefs.
    /// </summary>
    /// <param name="level">The volume level (expected range: 0.0001 to 1.0).</param>
    public void SetMusicVolume(float level)
    {
        _audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", level);
    }

    private void LoadVolumeSettings()
    {
        // Get saved volume levels or default to 1.0 (max volume)
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.2f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);

        // Apply saved volume levels
        _audioMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20f);
        _audioMixer.SetFloat("soundFXVolume", Mathf.Log10(sfxVolume) * 20f);
        _audioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20f);

        // Set slider positions to match the saved values
        if (masterSlider) masterSlider.value = masterVolume;
        if (sfxSlider) sfxSlider.value = sfxVolume;
        if (musicSlider) musicSlider.value = musicVolume;
    }
}
