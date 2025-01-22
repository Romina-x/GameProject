using UnityEngine;
using UnityEngine.AI; // For NavMeshAgent if used for movement

public class Animal : MonoBehaviour
{
    public Transform PlayerTarget; // Assigned when the animal is freed
    private NavMeshAgent _navMeshAgent; // Optional for smoother movement

    private bool _isFollowing = false;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_isFollowing && PlayerTarget != null)
        {
            // Follow player by updating destination to the player's position
            _navMeshAgent.SetDestination(PlayerTarget.position);
        }
    }

    public void StartFollowing(Transform player)
    {
        PlayerTarget = player;
        _isFollowing = true;
    }

    // public void StopFollowing()
    // {
    //     _isFollowing = false;
    //     if (_navMeshAgent != null)
    //     {
    //         _navMeshAgent.ResetPath();
    //     }
    // }
}
