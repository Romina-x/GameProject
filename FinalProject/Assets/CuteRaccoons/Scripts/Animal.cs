using UnityEngine;
using UnityEngine.AI;
using System;

public class Animal : MonoBehaviour
{
    // Event triggered when an animal is rescued
    public static event Action OnAnimalRescued;

    // Components
    public Transform PlayerTarget;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private bool _isFollowing = false;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isFollowing && PlayerTarget != null)
        {
            _navMeshAgent.SetDestination(PlayerTarget.position);
            float speed = _navMeshAgent.velocity.magnitude;
            _animator.SetBool("walk", speed > 0.3f);
        }
    }

    public void StartFollowing()
    {
        if (_isFollowing) return; // Prevent duplicate calls
        _isFollowing = true;
        OnAnimalRescued?.Invoke(); // Notify all subscribers
    }

    public void TeleportToPlayer()
    {
        if (!_isFollowing || PlayerTarget == null) return;

        Vector3 teleportPosition = PlayerTarget.position + PlayerTarget.forward * -1.5f;
        _navMeshAgent.Warp(teleportPosition);
    }
}
