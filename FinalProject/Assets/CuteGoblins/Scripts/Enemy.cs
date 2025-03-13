using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main controller for enemy movement and combat.
/// Handles the enemy's health, attack, movement, and death logic.
/// </summary>
public class Enemy : MonoBehaviour, IDamageable, IHealthSubject
{
    // Components assigned in unity editor
    public AttackRadius AttackRadius;
    public EnemyScriptableObject EnemyData;
    public GameObject PoofPrefab;

    private Animator _animator;
    private EnemyMovement _movement;
    private UnityEngine.AI.NavMeshAgent _agent;

    // Animation hashes
    private int _attackTriggerHash;
    private int _diedTriggerHash;
    private int _gotHitTriggerHash;

    private int _maxHealth = 100;
    private int _health;
    private bool _canAttack = true;
    private int _score;

    // Observers
    private List<IHealthObserver> _healthObservers = new List<IHealthObserver>();

    // Sound FX
    [SerializeField] private AudioClip _getHitClip;
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private AudioClip _vanishClip;

    // Event for enemy defeat
    public event Action<int> OnDefeated;

    //Properties
    public bool CanAttack { get { return _canAttack; } }

    private void Awake()
    {
        // Initialise attack hashes
        _attackTriggerHash = Animator.StringToHash("attack");
        _diedTriggerHash = Animator.StringToHash("died");
        _gotHitTriggerHash = Animator.StringToHash("gotHit");

        AttackRadius.SubscribeToAttackEvent(OnAttack);
        _animator = GetComponent<Animator>();
        _movement = GetComponent<EnemyMovement>();
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start()
    {
        SetupEnemyFromData();
        NotifyHealthObservers(); // Put health bars to the start health
    }

    /// <summary>
    /// Event triggered in attack radius. Plays the sound and animation for attack.
    /// </summary>
    /// <param name="target">The target to be attacked.</param>
    private void OnAttack(IDamageable target)
    {
        _animator.SetTrigger(_attackTriggerHash);
        SoundFXManager.instance.PlaySoundFX(_attackClip, transform, 1f);
    }

    /// <summary>
    /// Configures the enemy's properties using data from the ScriptableObject.
    /// </summary>
    void SetupEnemyFromData()
    {
        // Configure NavMeshAgent using ScriptableObject data
        _agent.speed = EnemyData.speed;
        _agent.acceleration = EnemyData.acceleration;
        _agent.angularSpeed = EnemyData.angularSpeed;
        _agent.stoppingDistance = EnemyData.stoppingDistance;
        _agent.avoidancePriority = EnemyData.avoidancePriority;
        _agent.height = EnemyData.height;
        _agent.radius = EnemyData.radius;

        // Set the movement update speed from the enemy data
        _movement.UpdateSpeed = EnemyData.AIUpdateInterval;

        _health = EnemyData.health;
        _maxHealth = EnemyData.health;
        _score = EnemyData.score;
        AttackRadius.Collider.radius = EnemyData.attackRadius;
        AttackRadius.AttackDelay = EnemyData.attackDelay;
        AttackRadius.Damage = EnemyData.damage;

    }

    /// <summary>
    /// Reduces the enemy's health by the given amount and handles death if health drops to zero.
    /// </summary>
    /// <param name="damage">The amount of damage to apply to the enemy.</param>
    public void TakeDamage(int damage)
    {
        // Take damage
        _health -= damage;
        NotifyHealthObservers(); // Notify observers of health change
        SoundFXManager.instance.PlaySoundFX(_getHitClip, transform, 1f);

        // Get hit if not dead
        if (_health > 0)
        {
            _animator.SetTrigger(_gotHitTriggerHash);
            StartCoroutine(DisableAttackDuringAnimation());
        }
        // Handle death when health below 0
        if (_health <= 0)
        {
            _canAttack = false;
            _animator.SetTrigger(_diedTriggerHash);
            _movement.StopFollowingOnDeath();
            AttackRadius.UnsubscribeFromAttackEvent(OnAttack);
            StartCoroutine(DelayedDeath());
        }
    }

    /// <summary>
    /// Delays the death process by a set amount of time before destroying the enemy.
    /// </summary>
    /// <returns>A coroutine that delays the death sequence.</returns>
    private IEnumerator DelayedDeath()
    {
        // Delay between the enemy falling and them disappearing
        yield return new WaitForSeconds(1.8f);

        if (PoofPrefab != null)
        {
            // Instantiate the poof effect at the enemy's position
            Instantiate(PoofPrefab, transform.position, Quaternion.identity);
        }
        SoundFXManager.instance.PlaySoundFX(_vanishClip, transform, 1f);

        // Raise the event for enemy defeat
        OnDefeated?.Invoke(_score);

        Destroy(gameObject);
    }

    /// <summary>
    /// Prevents enemies from attackin while they have been hit.
    /// </summary>
    /// <returns>Attack disable coroutine.</returns>
    private IEnumerator DisableAttackDuringAnimation()
    {
        _canAttack = false; // Disable attacks

        // Wait for the length of the "get hit" animation
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;
        yield return new WaitForSeconds(animationLength);

        _canAttack = true; // Re-enable attacks
    }

    // IHealthSubject interface methods
    public void RegisterHealthObserver(IHealthObserver observer)
    {
        _healthObservers.Add(observer);
    }

    public void UnregisterHealthObserver(IHealthObserver observer)
    {
        _healthObservers.Remove(observer);
    }

    public void NotifyHealthObservers()
    {
        foreach (IHealthObserver observer in _healthObservers)
        {
            observer.OnNotify(_maxHealth, _health);
        }
    }
    
    public Transform GetTransform()
    {
        return transform;
    }

    public string GetName()
    {
        return "Enemy";
    }
}
