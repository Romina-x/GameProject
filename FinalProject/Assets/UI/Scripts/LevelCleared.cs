using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Manages the level cleared screen which displays the player's score and time.
/// </summary>
public class LevelCleared : MonoBehaviour
{
    [SerializeField] private GameTimer _timer;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private AudioClip _levelClearedClip;


    public void Setup()
    {
        // Activate the UI screen and change the game state
        gameObject.SetActive(true);
        LevelManager.Instance.SetGameState(LevelState.LevelCleared);

        // Play sound effect
        SoundFXManager.instance.PlaySoundFX(_levelClearedClip, transform, 1f);

        // Get the time from the game timer and convert it to minutes and seconds
        float elapsedTime = _timer.ElapsedTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        _timeText.text = $"{minutes:D2}:{seconds:D2}";

        // Get score from the score manager and add on the elapsed time
        float finalScore = ScoreManager.Instance.Score + elapsedTime;
        _scoreText.text = $"{finalScore:N0}";
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
