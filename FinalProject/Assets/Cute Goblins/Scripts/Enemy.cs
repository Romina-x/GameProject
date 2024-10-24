using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private EnemyMovement movement;
    private UnityEngine.AI.NavMeshAgent agent;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
    }
}