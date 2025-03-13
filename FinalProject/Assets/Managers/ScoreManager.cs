using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int _score = 0;

    public int Score { get { return _score; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.OnDefeated += OnEnemyDefeated;
        }
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.OnDefeated -= OnEnemyDefeated;
        }
    }

    private void OnEnemyDefeated(int score)
    {
        _score += score;
        Debug.Log("New Score: " + _score);
    }
}
