using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]

public class EnemyScriptableObject : ScriptableObject
{
    public float speed;
    public float acceleration;
    public float angularSpeed;
    public int health;
    public float stoppingDistance;
    public int areaMask;
    public int avoidancePriority;
    public float baseOffset;
    public UnityEngine.AI.ObstacleAvoidanceType obstacleAvoidanceType;

    // Add height and radius for NavMeshAgent
    public float height;
    public float radius;

    public float AIUpdateInterval;
}