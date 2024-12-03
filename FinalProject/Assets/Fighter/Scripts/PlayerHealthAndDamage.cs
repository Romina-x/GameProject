using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles player attack behaviour and damage taking.
public class PlayerHealthAndDamage : MonoBehaviour, IDamageable
{
    // Player components
    private Animator _animator;
    private PlayerInput _playerInput;

    // State flags
    private bool _isDead = false;
    private bool _attacked = false;

    // Animator hashes
    private int _diedTriggerHash;
    private int _attackTriggerHash;
    private int _gotHitTriggerHash;

    // Player health
    private int _health = 300;
    private int _maxHealth = 300;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private GameOver _gameOverScreen; 

    void Awake()
    {
        // Retrieve components
        _animator = GetComponent<Animator>();     
        _playerInput = new PlayerInput();

        // Setup animator hashes
        _attackTriggerHash = Animator.StringToHash("attack");
        _gotHitTriggerHash = Animator.StringToHash("gotHit");
        _diedTriggerHash = Animator.StringToHash("died");

        // Subscribe to attack input action
        _playerInput.CharacterControls.Attack.performed += OnAttack;
        _healthBar.UpdateHealthBar(_maxHealth, _health);
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack button pressed");
        _animator.SetTrigger(_attackTriggerHash);
    }

    // Called every time the player gets hit by an enemy
    public void TakeDamage(int damage)
    {
        if (_isDead) return;  // Prevent taking damage after death

        // Take damage
        _health -= damage;

        // Update health bar
        _healthBar.UpdateHealthBar(_maxHealth, _health);

        if (_health > 0) 
        {
            _animator.SetTrigger(_gotHitTriggerHash);
        }

        if (_health <= 0)
        {
            _isDead = true;
            _animator.SetTrigger(_diedTriggerHash);
            _gameOverScreen.Setup();
        }
    }

    // Called when the player collects a heart 
    public void AddHealth(int amount)
    {
        if (_isDead) return; // Don't add health if the player is dead

        _health = Mathf.Clamp(_health + amount, 0, _maxHealth);
        _healthBar.UpdateHealthBar(_maxHealth, _health);
    }
    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    // IDamageable interface methods
    public Transform GetTransform()
    {
        return transform;
    }

    public string GetName()
    {
        return "Player";
    }
}
