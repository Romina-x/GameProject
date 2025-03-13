using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Manages the ranged enemy attack radius, inheriting from <see cref="AttackRadius"/>.
/// When the player enters this radius, the enemy begins attacking.
/// </summary>
public class RangedAttackRadius : AttackRadius
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Arrow _arrowPrefab;
    private Vector3 _arrowSpawnOffset = new Vector3(0.27f, 0.25f, 0); // Position to spawn the arrow relative to the enemy's position.
    private ObjectPool _arrowPool;
    private Arrow _arrow;

    protected override void Awake()
    {
        base.Awake();
        // Create new arrow pool with arrow prefab, attack delay and arrow lifetime.
        _arrowPool = ObjectPool.CreateInstance(_arrowPrefab, Mathf.CeilToInt((1 / _attackDelay) * _arrowPrefab.AutoDestroyTime));
    }

    /// <summary>
    /// The attack coroutine that handles shooting arrows at the player.
    /// The enemy stops moving while attacking, and arrows are shot based on attack delay.
    /// </summary>
    protected override IEnumerator Attack()
    {
        WaitForSeconds Wait = new WaitForSeconds(_attackDelay);

        yield return Wait;

        // Continue attacking while there is still a target
        while (_damageable != null)
        {
            _agent.isStopped = true;

            // Get an available arrow from the pool
            PoolableObject poolableObject = _arrowPool.GetObject();
            if (poolableObject != null)
            {
                // Set up the arrow's properties
                _arrow = poolableObject.GetComponent<Arrow>();
                _arrow.Damage = _damage;
                _arrow.transform.position = transform.TransformPoint(_arrowSpawnOffset);
                _arrow.transform.rotation = _agent.transform.rotation;

                // Apply force to the arrow in the direction that the enemy is facing
                _arrow.Rigidbody.AddForce(_agent.transform.forward * _arrowPrefab.MoveSpeed, ForceMode.VelocityChange);
                InvokeOnAttack(_damageable);
            }
            
            yield return Wait;
        }

        _attackCoroutine = null;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

}
