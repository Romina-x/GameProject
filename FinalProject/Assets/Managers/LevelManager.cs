using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to manage the transition between states of a level: GameOver, Playing etc.
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; } // Single instance of this class to be accessed

    [SerializeField]
    private PlayerStateMachine _player;
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
        SetGameState(LevelState.Playing);
    }

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

    // Enables or disables movement of all entities in the level
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

