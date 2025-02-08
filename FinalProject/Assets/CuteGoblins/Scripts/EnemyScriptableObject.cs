using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]

// Enemy configurations: Stats and NavMeshAgent settings
public class EnemyScriptableObject : ScriptableObject
{
    // Enemy stats
    public int health ;
    public float attackDelay;
    public int damage;
    public float attackRadius;
    public int score;

    // Navmesh agent configs
    public float AIUpdateInterval;
    public float acceleration;
    public float angularSpeed;
    public int areaMask;
    public int avoidancePriority;
    public float baseOffset;
    public float height;
    public UnityEngine.AI.ObstacleAvoidanceType obstacleAvoidanceType;
    public float radius;
    public float speed;
    public float stoppingDistance;
}