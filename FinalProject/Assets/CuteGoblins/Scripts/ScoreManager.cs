using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's score during a level.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    // Singleton instance of ScoreManager
    public static ScoreManager Instance { get; private set; }
    private int _score = 0;

    // Properties
    public int Score { get { return _score; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Subscribe to defeat event of all enemies in the scene
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.OnDefeated += OnEnemyDefeated;
        }
        Debug.Log(_score);
    }

    private void OnDestroy()
    {   
        // Unsubscribe from all enemies
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.OnDefeated -= OnEnemyDefeated;
        }
    }

    /// <summary>
    /// Updates the score when an enemy is defeated.
    /// </summary>
    /// <param name="score">Score value of the enemy.</param>
    private void OnEnemyDefeated(int score)
    {
        _score += score;
        Debug.Log(_score);
    }
}
