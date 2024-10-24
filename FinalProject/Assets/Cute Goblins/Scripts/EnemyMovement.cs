using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float updateSpeed = 0.1f;
    private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]
    private Animator animator;

    private int isMovingHash;

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
            agent.SetDestination(target.transform.position);
            yield return wait;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(isMovingHash, agent.velocity.magnitude > 0.01f);
    }
}
