using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyMovement : EnemyMovement
{
    private Coroutine _aimCoroutine;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnPlayerEntered()
    {
        _agent.isStopped = true;
        
        // Start aiming coroutine if not already running
        if (_aimCoroutine == null)
        {
            _aimCoroutine = StartCoroutine(AimAtPlayer());
        }
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit(); // Ensures the enemy returns to origin

        // Stop aiming coroutine if running
        if (_aimCoroutine != null)
        {
            StopCoroutine(_aimCoroutine);
            _aimCoroutine = null;
        }
    }

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

            yield return null; // Wait for the next frame
        }
    }
}
