using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float updateSpeed = 0.1f;  // This will be set from the Enemy class
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    private int isMovingHash;
    private bool isMoving = false;

    private void Awake() 
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();

        isMovingHash = Animator.StringToHash("isMoving");
    }

    void Start()
    {
        StartCoroutine(FollowTarget());
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

    void Update()
    {
        // Set the "isMoving" animation state based on NavMeshAgent's velocity
        
    }
}