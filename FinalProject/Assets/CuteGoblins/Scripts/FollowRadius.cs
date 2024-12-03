using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When the player enters this radius, the enemy begins following them
public class FollowRadius : MonoBehaviour
{
    // Events for player entering and exiting the follow radius
    private event System.Action PlayerEnter;
    private event System.Action PlayerExit;

    // Public methods for event subscription
    public void SubscribeToPlayerEnter(System.Action handler)
    {
        PlayerEnter += handler;
    }

    public void UnsubscribeFromPlayerEnter(System.Action handler)
    {
        PlayerEnter -= handler;
    }

    public void SubscribeToPlayerExit(System.Action handler)
    {
        PlayerExit += handler;
    }

    public void UnsubscribeFromPlayerExit(System.Action handler)
    {
        PlayerExit -= handler;
    }

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
