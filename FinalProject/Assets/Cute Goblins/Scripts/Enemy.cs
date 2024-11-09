using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{   
    public AttackRadius attackRadius;
    public Animator animator;
    public EnemyScriptableObject enemyData;
    private EnemyMovement movement;
    private UnityEngine.AI.NavMeshAgent agent;
    private int attackTriggerHash;
    private int diedTriggerHash;
    private int gotHitTriggerHash;
    public int health = 100;


    private void Awake(){
        attackTriggerHash = Animator.StringToHash("attack");
        diedTriggerHash = Animator.StringToHash("died");
        gotHitTriggerHash = Animator.StringToHash("gotHit");

        attackRadius.OnAttack += OnAttack;
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void OnAttack(IDamageable target){
        animator.SetTrigger(attackTriggerHash);
    }

    void Start()
    {
        SetupEnemyFromData();
    }

    void SetupEnemyFromData()
    {
        // Configure NavMeshAgent using ScriptableObject data
        agent.speed = enemyData.speed;
        agent.acceleration = enemyData.acceleration;
        agent.angularSpeed = enemyData.angularSpeed;
        agent.stoppingDistance = enemyData.stoppingDistance;
        agent.avoidancePriority = enemyData.avoidancePriority;
        agent.height = enemyData.height;
        agent.radius = enemyData.radius;

        // Set the movement update speed from the enemy data
        movement.updateSpeed = enemyData.AIUpdateInterval;

        health = enemyData.health;
        attackRadius.collider.radius = enemyData.attackRadius;
        attackRadius.attackDelay = enemyData.attackDelay;
        attackRadius.damage = enemyData.damage;
    }

    public void TakeDamage(int damage){
        health -= damage;
        if (health > 0) {
            animator.SetTrigger(gotHitTriggerHash);
        }
        if (health <= 0){
            Debug.Log("health < 0");
            animator.SetTrigger(diedTriggerHash);
            movement.StopFollowingOnDeath();
            attackRadius.StopAttackCoroutine();
            // gameObject.SetActive(false);
        }
    }

    public Transform GetTransform(){
        return transform;
    }

    public string GetName(){
        return "Enemy";
    }
}