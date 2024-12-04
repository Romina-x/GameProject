using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
