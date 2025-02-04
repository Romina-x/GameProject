using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to manage the transition between states of a level: GameOver, Playing etc.
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; } // Single instance of this class to be accessed

    // Level states
    public enum LevelState {
        Playing, 
        Paused, 
        LevelComplete, 
        GameOver 
    }

    private LevelState _currentState;

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
        SetGameState(GameState.Playing);
    }

    public void SetGameState(GameState newState)
    {
        _currentState = newState;
        Debug.Log("Game State changed to: " + _currentState);

        switch (_currentState)
        {
            case GameState.Playing:
                EnableGameplay(true);
                break;

            case GameState.Paused:
                EnableGameplay(false);
                break;

            case GameState.LevelComplete:
                EnableGameplay(false);
                break;

            case GameState.GameOver:
                EnableGameplay(false);
                break;
        }
    }

    private void EnableGameplay(bool isEnabled)
    {
        // Disable player movement & enemy AI
        _player.EnableMovement(isEnabled);

        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.EnableMovement(isEnabled);
        }
    }
}

