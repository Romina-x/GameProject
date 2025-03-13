using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the sound settings menu UI screen.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonClip;

    public void Setup()
    {
        gameObject.SetActive(true);
        LevelManager.Instance.SetGameState(LevelState.Paused);
        SoundFXManager.instance.PlaySoundFX(_buttonClip, transform, 1f);
    }

    public void ExitButton()
    {
        SoundFXManager.instance.PlaySoundFX(_buttonClip, transform, 1f);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        LevelManager.Instance.SetGameState(LevelState.Playing);
    }
}
