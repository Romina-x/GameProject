using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCleared : MonoBehaviour
{
    [SerializeField] private GameTimer _timer;
    [SerializeField] private TextMeshProUGUI _timeText;

    public void Setup()
    {
        gameObject.SetActive(true);
        LevelManager.Instance.SetGameState(LevelState.LevelCleared);

        float elapsedTime = _timer.ElapsedTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        _timeText.text = $"{minutes:D2}:{seconds:D2}";
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
