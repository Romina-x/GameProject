using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
        LevelManager.Instance.SetGameState(LevelState.Paused);
    }

    public void ExitButton()
    {
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        LevelManager.Instance.SetGameState(LevelState.Playing);
    }
}
