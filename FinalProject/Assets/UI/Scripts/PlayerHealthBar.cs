using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's health bar display on the UI.
/// </summary>
public class PlayerHealthBar : MonoBehaviour, IHealthObserver
{
    [SerializeField] private Slider _healthBarSlider; 

    // Player to observe
    [SerializeField] private PlayerHealthAndDamage _playerHealth;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main ?? FindObjectOfType<Camera>();
    }

    /// <summary>
    /// Updates the health bar UI based on the player's current health.
    /// </summary>
    /// <param name="maxHealth">The maximum health of the player.</param>
    /// <param name="currentHealth">The current health of the player.</param>
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        // Set the slider's value between 0 and 1
        _healthBarSlider.value = currentHealth / maxHealth;
    }

    // Make sure the bar updates to stay facing the camera
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

    // IHealthObserver interface methods
    public void OnNotify(float maxHealth, float currentHealth)
    {
        UpdateHealthBar(maxHealth, currentHealth);
    }

    // Register as an observer of the subject
    private void OnEnable()
    {
        _playerHealth.RegisterHealthObserver(this);
    }

    private void OnDisable()
    {
        _playerHealth.UnregisterHealthObserver(this);
    }
}
