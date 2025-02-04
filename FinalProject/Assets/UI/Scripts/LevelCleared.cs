using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCleared : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
        LevelManager.Instance.SetGameState(LevelState.LevelCleared);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

}
