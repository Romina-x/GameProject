using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the health bar display for an enemy.
/// </summary>
public class EnemyHealthBar : MonoBehaviour, IHealthObserver
{
    [SerializeField] private Image _healthBarSprite;

    // Enemy to observe
    [SerializeField] private Enemy _enemyHealth;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main ?? FindObjectOfType<Camera>();
    }

    /// <summary>
    /// Updates the health bar's fill amount based on the enemy's current health.
    /// </summary>
    /// <param name="maxHealth">The maximum health of the enemy.</param>
    /// <param name="currentHealth">The current health of the enemy.</param>
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthBarSprite.fillAmount = currentHealth / maxHealth;
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

    // Register as an observer of enemy health
    private void OnEnable()
    {
        _enemyHealth.RegisterHealthObserver(this);
    }

    private void OnDisable()
    {
        _enemyHealth.UnregisterHealthObserver(this);
    }
}
