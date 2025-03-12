using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IDefeatObserver
{
    public static ScoreManager Instance { get; private set; }
    private int _score = 0;

    public int Score { get { return _score; } }

    private void Awake()
    {
        // Destroy other instances of this class
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Find all enemies in the scene and register as an observer
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            if (enemy != null)
            {
                enemy.RegisterDefeatObserver(this);
            }
        }
    }

    // IDefeatObserver method
    // Recieves the score of defeated enemy and adds to the total
    public void OnNotify(int score)
    {
        _score += score;
        Debug.Log(_score);
    }
}
