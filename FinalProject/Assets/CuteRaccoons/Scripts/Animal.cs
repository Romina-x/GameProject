using UnityEngine;
using UnityEngine.AI; // For NavMeshAgent if used for movement
using System.Collections.Generic;

public class Animal : MonoBehaviour
{
    // Components 
    public Transform PlayerTarget; 
    private NavMeshAgent _navMeshAgent; 
    private Animator _animator;

    private bool _isFollowing = false;
    private List<IRescueObserver> _rescueObservers = new List<IRescueObserver>();

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isFollowing && PlayerTarget != null)
        {
            // Follow player by updating destination to the player's position
            _navMeshAgent.SetDestination(PlayerTarget.position);

            float speed = _navMeshAgent.velocity.magnitude; // Current movement speed
            _animator.SetBool("walk", speed > 0.3f);
        }
    }

    public void StartFollowing()
    {
        _isFollowing = true;
        NotifyRescueObservers();
    }

    public void TeleportToPlayer()
    {
        if (!_isFollowing)
    {
        // Prevent teleporting if the animal is still in the cage
        return;
    }
        if (PlayerTarget == null) return;

        // Teleport the animal directly to the player's position
        Vector3 teleportPosition = PlayerTarget.position;

        // Optional: Offset the teleport position slightly behind or to the side of the player
        teleportPosition += PlayerTarget.forward * -1.5f;

        // Use NavMeshAgent.Warp to instantly move the agent
        _navMeshAgent.Warp(teleportPosition);
    }

    public void EnableMovement(bool isEnabled){
        _navMeshAgent.isStopped = !isEnabled;
        _animator.speed = isEnabled ? 1 : 0;
        _navMeshAgent.velocity = Vector3.zero;
    }

    // IRescueSubject interface methods
    public void RegisterRescueObserver(IRescueObserver observer)
    {
        _rescueObservers.Add(observer);
    }

    public void UnregisterRescueObserver(IRescueObserver observer)
    {
        _rescueObservers.Remove(observer);
    }

    public void NotifyRescueObservers()
    {
        foreach (var observer in _rescueObservers)
        {
            observer.OnAnimalRescued();
        }
    }
}
