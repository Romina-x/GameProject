using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls enemy movement and animation, including following the player and returning to its spawn point. 
/// </summary>
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    // Components assigned in the unity editor
    public Transform Target;
    public FollowRadius FollowRadius;

    protected UnityEngine.AI.NavMeshAgent _agent;
    protected Animator _animator;

    // Settings
    protected float _updateSpeed = 0.1f;
    protected float _returnThreshold = 0.5f;

    // Animation state  
    protected int _isMovingHash;
    protected bool _isMoving = false;

    protected Coroutine _followCoroutine;
    protected Vector3 _originalPosition;

    // Properties
    public float UpdateSpeed { set { _updateSpeed = value; } }

    protected virtual void Awake() 
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _isMovingHash = Animator.StringToHash("isMoving");
        
        if (FollowRadius != null) 
        {
            FollowRadius.PlayerEnter += OnPlayerEntered;
            FollowRadius.PlayerExit += OnPlayerExit;
        }

        _originalPosition = transform.position;
    }

    /// <summary>
    /// Event triggered from FollowRadius collider when the player enters.
    /// Starts the follow player coroutine.
    /// </summary>
    protected virtual void OnPlayerEntered()
    {
        if (_followCoroutine == null)
        {
            _followCoroutine = StartCoroutine(FollowTarget());
            _agent.isStopped = false;  // Allow the NavMeshAgent to move
        }
    }

    /// <summary>
    /// Event triggered from FollowRadius collider when the player exits.
    /// Stops the follow player coroutine and starts return to origin.
    /// </summary>
    protected virtual void OnPlayerExit()
    {
        if (_followCoroutine != null)
        {
            StopCoroutine(_followCoroutine);
            _followCoroutine = null;
        }

        StartCoroutine(ReturnToOrigin());
    }

    /// <summary>
    /// Coroutine that makes the enemy follow the player.
    /// </summary>
    protected IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(_updateSpeed);
        while (enabled)
        {
            if (Target != null)
            {
                _isMoving = _agent.velocity.magnitude > 0.1f;
                _animator.SetBool(_isMovingHash, _isMoving);
                _agent.SetDestination(Target.position);  // Update enemy movement towards target

            }
            yield return wait;
        }
    }

    /// <summary>
    /// Coroutine to return the enemy to its original position once the player is out of range.
    /// </summary>
    protected IEnumerator ReturnToOrigin()
    {
        _agent.SetDestination(_originalPosition);
        // Wait until the enemy is close enough to the original position
        float distanceToSpawn = Vector3.Distance(transform.position, _originalPosition);
        while (distanceToSpawn > _returnThreshold)
        {
            bool isMoving = _agent.velocity.magnitude > 0.1f;
            _animator.SetBool(_isMovingHash, isMoving);
            yield return null;
        }

        _agent.isStopped = true;
        _animator.SetBool(_isMovingHash, false);
    }

    /// <summary>
    /// Stops enemy movement during the death sequence.
    /// </summary>
    public void StopFollowingOnDeath()
    {
        if (_followCoroutine != null)
        {
            StopCoroutine(_followCoroutine);  // Stop the coroutine
            _followCoroutine = null;
        }
        _agent.isStopped = true;  // Stop the NavMeshAgent from moving
    }

}