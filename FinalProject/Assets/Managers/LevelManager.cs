using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton to manage the transitions between states of a level: GameOver, Playing etc.
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; } // Singleton instance of this class to be accessed
    private LevelState _currentState;

    // Properties
    public LevelState CurrentState { get { return _currentState; } }

    private void Awake()
    {
        // Only ever have once instance. Destroy this if there already is one
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initial state at the start of each level is playing
        SetGameState(LevelState.Playing);
    }

    /// <summary>
    /// Changes the game state and enables or disables gameplay accordingly.
    /// </summary>
    /// <param name="newState">The new level state to transition to.</param>
    public void SetGameState(LevelState newState)
    {
        _currentState = newState;
        Debug.Log("Game State changed to: " + _currentState);

        switch (_currentState)
        {
            case LevelState.Playing:
                EnableGameplay(true);
                break;

            case LevelState.Paused:
                EnableGameplay(false);
                break;

            case LevelState.LevelCleared:
                EnableGameplay(false);
                break;

            case LevelState.GameOver:
                EnableGameplay(false);
                break;
        }
    }

    /// <summary>
    /// Enables or disables gameplay by adjusting the time scale.
    /// </summary>
    /// <param name="isEnabled">True to enable gameplay, false to pause it.</param>
    private void EnableGameplay(bool isEnabled)
    {
        // Pause the level by setting the timescale to 0
        if (isEnabled)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}

