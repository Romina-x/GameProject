using UnityEngine;
using UnityEngine.AI; // For NavMeshAgent if used for movement

public class Animal : MonoBehaviour
{
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
            // Follow player by updating destination to the player's position
            _navMeshAgent.SetDestination(PlayerTarget.position);

            float speed = _navMeshAgent.velocity.magnitude; // Current movement speed
            _animator.SetBool("walk", speed > 0.2f);
        }
    }

    public void StartFollowing()
    {
        _isFollowing = true;
    }
}
