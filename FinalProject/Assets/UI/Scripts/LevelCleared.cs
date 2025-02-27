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
    [SerializeField] private TextMeshProUGUI _scoreText;


    public void Setup()
    {
        // Activate the UI screen and change the game state
        gameObject.SetActive(true);
        LevelManager.Instance.SetGameState(LevelState.LevelCleared);

        // Get the time from the game timer and convert it to minutes and seconds
        float elapsedTime = _timer.ElapsedTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        _timeText.text = $"{minutes:D2}:{seconds:D2}";

        // Get score from the score manager and add on the elapsed time
        float finalScore = ScoreManager.Instance.Score + elapsedTime;
        _scoreText.text = $"{finalScore:N0}";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

}
