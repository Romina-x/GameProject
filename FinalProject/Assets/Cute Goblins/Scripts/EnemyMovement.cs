using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float updateSpeed = 0.1f;  // This will be set from the Enemy class
    private float returnThreshold = 0.5f;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    private int isMovingHash;
    private bool isMoving = false;
    private Coroutine followCoroutine;
    public FollowRadius followRadius;
    private Vector3 originalPosition;

    private void Awake() 
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();

        isMovingHash = Animator.StringToHash("isMoving");
        
        if (followRadius != null) 
        {
            followRadius.PlayerEnter += OnPlayerEntered;
            followRadius.PlayerExit += ReturnToSpawn;
        }

        originalPosition = transform.position;
    }

    private void OnPlayerEntered()
    {
        Debug.Log("OnPlayerEntered");
        StartFollowing();
    }

    // Method to start following the target
    public void StartFollowing() 
    {
        if (followCoroutine == null) 
        {
            followCoroutine = StartCoroutine(FollowTarget());
            agent.isStopped = false;  // Allow the NavMeshAgent to move
        }
    }

    private void ReturnToSpawn(){
        if (followCoroutine != null){
            StopCoroutine(followCoroutine);
            followCoroutine = null;
        }

        StartCoroutine(ReturnToOrigin());
    }

    private IEnumerator FollowTarget(){
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        while(enabled){
            if (target != null)
            {
                isMoving = agent.velocity.magnitude > 0.2f;
                animator.SetBool(isMovingHash, isMoving);
                agent.SetDestination(target.position);  // Update enemy movement towards target
            }
            yield return wait;
        }
    }

    private IEnumerator ReturnToOrigin(){
        agent.SetDestination(originalPosition);
        // Wait until the enemy is close enough to the original position
        float distanceToSpawn = Vector3.Distance(transform.position, originalPosition);
        while (distanceToSpawn > returnThreshold)
        {
            bool isMoving = agent.velocity.magnitude > 0.2f;
            animator.SetBool(isMovingHash, isMoving);
            yield return null;
        }

        agent.isStopped = true;
        animator.SetBool(isMovingHash, false);
    }

    public void StopFollowingOnDeath() {
        if (followCoroutine != null) {
            StopCoroutine(followCoroutine);  // Stop the coroutine
            followCoroutine = null;
        }
        agent.isStopped = true;  // Stop the NavMeshAgent from moving
    }
}