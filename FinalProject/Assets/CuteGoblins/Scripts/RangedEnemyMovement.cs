using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Manages the movement and aiming behavior of a ranged enemy, inheriting from <see cref="EnemyMovement"/>
/// </summary>
public class RangedEnemyMovement : EnemyMovement
{
    private Coroutine _aimCoroutine;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Called when the player enters the enemy's detection range and starts aiming at the player.
    /// </summary>
    protected override void OnPlayerEntered()
    {
        _agent.isStopped = true;

        // Start aiming coroutine
        if (_aimCoroutine == null)
        {
            _aimCoroutine = StartCoroutine(AimAtPlayer());
        }
    }

    /// <summary>
    /// Called when the player exits the enemy's detection range and stops the aiming coroutine.
    /// </summary>
    protected override void OnPlayerExit()
    {
        // Stop aiming coroutine
        if (_aimCoroutine != null)
        {
            StopCoroutine(_aimCoroutine);
            _aimCoroutine = null;
        }
    }

    /// <summary>
    /// A coroutine that continuously aims the enemy towards the player's position.
    /// </summary>
    private IEnumerator AimAtPlayer()
    {
        while (true) // Keep aiming continuously
        {
            if (Target != null)
            {
                Vector3 direction = (Target.position - transform.position).normalized;
                direction.y = 0; // Keep rotation flat
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }

            yield return null;
        }
    }
}
