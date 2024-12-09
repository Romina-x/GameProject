using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

// Controller for enemy movement input
public class EnemyMovement : MonoBehaviour
{
    // Components assigned in the unity editor
    public Transform Target;
    public FollowRadius FollowRadius;

    private UnityEngine.AI.NavMeshAgent _agent;
    private Animator _animator;

    // Settings
    private float _updateSpeed = 0.1f;
    private float _returnThreshold = 0.5f;

    // Animation state  
    private int _isMovingHash;
    private bool _isMoving = false;

    private Coroutine _followCoroutine;
    private Vector3 _originalPosition;

    // Properties
    public float UpdateSpeed { set { _updateSpeed = value; } }

    private void Awake() 
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _isMovingHash = Animator.StringToHash("isMoving");
        
        if (FollowRadius != null) 
        {
            FollowRadius.SubscribeToPlayerEnter(OnPlayerEntered);
            FollowRadius.SubscribeToPlayerExit(OnPlayerExit);
        }

        _originalPosition = transform.position;
    }

    private void OnPlayerEntered()
    {
        StartFollowing();
    }

    // Method to start following the target
    public void StartFollowing() 
    {
        if (_followCoroutine == null) 
        {
            _followCoroutine = StartCoroutine(FollowTarget());
            _agent.isStopped = false;  // Allow the NavMeshAgent to move
        }
    }

    private void OnPlayerExit()
    {
        if (_followCoroutine != null)
        {
            StopCoroutine(_followCoroutine);
            _followCoroutine = null;
        }

        StartCoroutine(ReturnToOrigin());
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(_updateSpeed);
        while(enabled){
            if (Target != null)
            {
                _isMoving = _agent.velocity.magnitude > 0.1f;
                _animator.SetBool(_isMovingHash, _isMoving);
                _agent.SetDestination(Target.position);  // Update enemy movement towards target
            }
            yield return wait;
        }
    }

    private IEnumerator ReturnToOrigin()
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