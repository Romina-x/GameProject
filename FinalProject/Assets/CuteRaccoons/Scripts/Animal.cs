using UnityEngine;
using UnityEngine.AI;
using System;

/// <summary>
/// Manages the movement and behaviour of an animal
/// </summary>
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

    /// <summary>
    /// Called every frame. If the animal has been rescued, it updates the animal's movement and animations to follow the player.
    /// </summary>
    void Update()
    {
        if (_isFollowing && PlayerTarget != null)
        {
            _navMeshAgent.SetDestination(PlayerTarget.position);
            float speed = _navMeshAgent.velocity.magnitude;
            _animator.SetBool("walk", speed > 0.3f);
        }
    }

    /// <summary>
    /// Called once the animal has been rescued. Allows it to begin moving.
    /// </summary>
    public void StartFollowing()
    {
        if (_isFollowing) return; // Prevent duplicate calls

        _isFollowing = true;
        OnAnimalRescued?.Invoke(); // Notify all subscribers of rescue
    }

    /// <summary>
    /// Teleports the animal back to the player if it gets stuck too far away.
    /// </summary>
    public void TeleportToPlayer()
    {
        if (!_isFollowing || PlayerTarget == null) return; // Do not teleport if the animal has not been rescued yet

        Vector3 teleportPosition = PlayerTarget.position + PlayerTarget.forward * -1.5f;
        _navMeshAgent.Warp(teleportPosition);
    }
}
