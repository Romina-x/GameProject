using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour, IHealthObserver
{
    [SerializeField] private PlayerHealthAndDamage _playerHealth;
    [SerializeField] private AudioClip _gameOverClip;

    public void Setup()
    {
        gameObject.SetActive(true);
        LevelManager.Instance.SetGameState(LevelState.GameOver);
        SoundFXManager.instance.PlaySoundFX(_gameOverClip, transform, 1f);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    
    // IHealthObserver interface methods
    public void OnNotify(float maxHealth, float currentHealth)
    {
        if (currentHealth <= 0) 
        {
            Setup();
        }
    }
    
    // Register this as an observer of player health
    private void Start()
    {
        gameObject.SetActive(false);
        _playerHealth.RegisterHealthObserver(this); 
    }

    private void OnDisable()
    {
        _playerHealth.UnregisterHealthObserver(this);
        LevelManager.Instance.SetGameState(LevelState.Playing);
    }
}
