using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonClip;
    public void Setup()
    {
        Debug.Log("Setting up pause menu");
        gameObject.SetActive(true);
        LevelManager.Instance.SetGameState(LevelState.Paused);
        SoundFXManager.instance.PlaySoundFX(_buttonClip, transform, 1f);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void ContinueButton()
    {
        SoundFXManager.instance.PlaySoundFX(_buttonClip, transform, 1f);
        gameObject.SetActive(false);
    }
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        LevelManager.Instance.SetGameState(LevelState.Playing);
    }
}
