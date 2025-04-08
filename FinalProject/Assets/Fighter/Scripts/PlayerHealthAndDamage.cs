using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles player attack behaviour and damage taking.
/// </summary>
public class PlayerHealthAndDamage : MonoBehaviour, IDamageable, IHealthSubject
{
    // Player components
    private Animator _animator;
    private PlayerInput _playerInput;

    // Observers
    private List<IHealthObserver> _observers = new List<IHealthObserver>();

    // State flags
    private bool _isDead = false;

    // Animator hashes
    private int _diedTriggerHash;
    private int _attackTriggerHash;
    private int _gotHitTriggerHash;

    // Player health
    private int _health = 300;
    private int _maxHealth = 300;

    // Sound
    [SerializeField] private AudioClip _axeSwingClip;
    [SerializeField] private AudioClip _getHitClip;

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

        NotifyHealthObservers(); // with start health
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        _animator.SetTrigger(_attackTriggerHash);
    }

    /// <summary>
    /// Adds health to the player when collecting a heart, makign sure it doesn't exceed the maximum.
    /// </summary>
    /// <param name="amount">The amount of health to add.</param> 
    public void AddHealth(int amount)
    {
        if (_isDead) return; // Don't add health if the player is dead

        _health = Mathf.Clamp(_health + amount, 0, _maxHealth);
        NotifyHealthObservers(); // Notfy all observers of health change
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

    /// <summary>
    /// Handles the player taking damage. This will subtract health and check if the player dies.
    /// </summary>
    /// <param name="damage">The amount of damage to apply to the player's health.</param>
    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        // Take damage
        _health -= damage;
        NotifyHealthObservers(); // Notify all observers of health change

        SoundFXManager.instance.PlaySoundFX(_getHitClip, transform, 1f);

        // Choose which animation to play if the player is alive or has died
        if (_health > 0)
        {
            _animator.SetTrigger(_gotHitTriggerHash);
        }
        if (_health <= 0)
        {
            _isDead = true;
            _animator.SetTrigger(_diedTriggerHash);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public string GetName()
    {
        return "Player";
    }

    // IHealthSubject interface methods
    public void RegisterHealthObserver(IHealthObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterHealthObserver(IHealthObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyHealthObservers()
    {
        foreach (IHealthObserver observer in _observers)
        {
            observer.OnNotify(_maxHealth, _health);
        }
    }
    
    // Animator event to play axe swing at the correct point in the animation
    public void PlayAxeSwingSound()
    {
        SoundFXManager.instance.PlaySoundFX(_axeSwingClip, transform, 1f);
    }

}
