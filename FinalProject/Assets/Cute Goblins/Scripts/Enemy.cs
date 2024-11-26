using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main controller for enemy movement and combat
public class Enemy : MonoBehaviour, IDamageable
{   
    // Components assigned in unity editor
    public AttackRadius AttackRadius;
    public EnemyScriptableObject EnemyData;
    public GameObject PoofPrefab;
    [SerializeField] private HealthBar _healthBar; 

    private Animator _animator;
    private EnemyMovement _movement;
    private UnityEngine.AI.NavMeshAgent _agent;

    // Animation hashes
    private int _attackTriggerHash;
    private int _diedTriggerHash;
    private int _gotHitTriggerHash;

    private int _maxHealth = 100;
    private int _health;
    

    // Enemy state
    private bool _isDefeated;

    // Events
    private static event System.Action OnEnemyDefeated;

    // Properties
    public bool IsDefeated { get { return _isDefeated; } }

    // Public event subscription methods
    public static void SubscribeToEnemyDefeated(System.Action handler)
    {
        OnEnemyDefeated += handler;
    }

    public static void UnsubscribeFromEnemyDefeated(System.Action handler)
    {
        OnEnemyDefeated -= handler;
    }

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
        _healthBar.UpdateHealthBar(_maxHealth, _health);
    }

    private void OnAttack(IDamageable target)
    {
        _animator.SetTrigger(_attackTriggerHash);
    }

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
        AttackRadius.Collider.radius = EnemyData.attackRadius;
        AttackRadius.AttackDelay = EnemyData.attackDelay;
        AttackRadius.Damage = EnemyData.damage;
    }

    public void TakeDamage(int damage)
    {
        // Take damage
        _health -= damage;

        // Update health bar
        _healthBar.UpdateHealthBar(_maxHealth, _health);

        // Get hit if not dead
        if (_health > 0) 
        {
            _animator.SetTrigger(_gotHitTriggerHash);
        }
        // Handle death when health below 0
        if (_health <= 0)
        {
            _isDefeated = true;
            _animator.SetTrigger(_diedTriggerHash);
            _movement.StopFollowingOnDeath();
            AttackRadius.UnsubscribeFromAttackEvent(OnAttack);
            //AttackRadius.StopAttackCoroutine();
            StartCoroutine(DelayedDeath());
            OnEnemyDefeated?.Invoke();
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    // Death sequence 
    private IEnumerator DelayedDeath()
    {
        // Delay between the enemy falling and them disappearing
        yield return new WaitForSeconds(1.8f); 
        Die();
    }

    public void Die()
    {
        if (PoofPrefab != null)
        {
            // Instantiate the poof effect at the enemy's position
            Instantiate(PoofPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    public string GetName()
    {
        return "Enemy";
    }
}