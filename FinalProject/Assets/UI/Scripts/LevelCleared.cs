using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCleared : MonoBehaviour
{
    public void Setup()
    {
        Debug.Log("Setting up");
        gameObject.SetActive(true);
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
