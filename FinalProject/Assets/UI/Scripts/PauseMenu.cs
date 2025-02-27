using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Setup()
    {
        Debug.Log("Setting up pause menu");
        gameObject.SetActive(true);
        LevelManager.Instance.SetGameState(LevelState.Paused);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void ContinueButton()
    {
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
