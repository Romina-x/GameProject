using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IHealthObserver
{
    [SerializeField] private Slider _healthBarSlider; // Reference to the Slider component

    // Player to observe
    [SerializeField] private PlayerHealthAndDamage _playerHealth;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main ?? FindObjectOfType<Camera>();
    }

    // Update the value of the slider based on health
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
