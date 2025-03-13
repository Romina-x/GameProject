using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the radius (sphere collider) around the enemy in which they follow the player, and notifies <see cref="EnemyMovement"/>.
/// </summary>
public class FollowRadius : MonoBehaviour
{
    // Events for player entering and exiting the follow radius
    public event System.Action PlayerEnter;
    public event System.Action PlayerExit;


    // Triggered when player enters the radius
    private void OnTriggerEnter(Collider other) 
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            PlayerEnter?.Invoke();
        }
    }

    // Triggered when the player leaves the radius
    private void OnTriggerExit(Collider other) {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            PlayerExit?.Invoke();
        }
    }
}
