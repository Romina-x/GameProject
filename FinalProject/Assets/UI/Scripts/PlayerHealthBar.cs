using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour, IHealthObserver
{
    [SerializeField] private Image _healthBarSprite;

    // Player to observe
    [SerializeField] private PlayerHealthAndDamage _playerHealth;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main ?? FindObjectOfType<Camera>();
    }

    // Update the fill of the bar based on health
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

    // Register as an observer of the subject
    private void OnEnable()
    {
        _playerHealth.RegisterObserver(this);
    }

    private void OnDisable()
    {
        _playerHealth.UnregisterObserver(this);
    }
}
