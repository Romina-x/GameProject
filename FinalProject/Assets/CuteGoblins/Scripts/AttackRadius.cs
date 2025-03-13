using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the enemy attack radius.
/// When the player enters this radius, the enemy begins attacking.
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    // Components
    public SphereCollider Collider;

    // Player object
    protected IDamageable _damageable;

    // Attack settings
    protected float _attackDelay = 0.5f;
    protected int _damage = 10;

    // Attack event
    protected event Action<IDamageable> OnAttack;
    protected Coroutine _attackCoroutine;

    // Properties
    public float AttackDelay { set { _attackDelay = value; } }
    public int Damage { set { _damage = value; } }

    protected virtual void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }

    /// <summary>
    /// Called when an object enters the attack radius.
    /// If the object is damageable, it starts the attack coroutine.
    /// </summary>
    /// <param name="other">The collider that entered the attack radius.</param>
    protected virtual void OnTriggerEnter(Collider other) 
    {
        _damageable = other.GetComponent<IDamageable>();

        // If object is a damageable
        if (_damageable != null)
        {
            // Begin attacking
            if(_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    /// <summary>
    /// Called when an object exits the attack radius.
    /// Stops attacking if the exiting object was the current target.
    /// </summary>
    /// <param name="other">The collider that exited the attack radius.</param>
    protected virtual void OnTriggerExit(Collider other) 
    {
        _damageable = other.GetComponent<IDamageable>();

        if (_damageable != null)
        {
            // Set damageable to null and stop the attack coroutine
            _damageable = null;
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    /// <summary>
    /// Continuously attacks the target while they remain in range.
    /// Waits for a specified delay between attacks.
    /// </summary>
    /// <returns>Attack coroutine</returns>
    protected virtual IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(_attackDelay);

        // Keep attacking as long as the player is within the radius and the enemy can attack
        while (_damageable != null)
        {
            if (GetComponentInParent<Enemy>().CanAttack)
            {
                OnAttack?.Invoke(_damageable);
                _damageable.TakeDamage(_damage);
            }
            yield return wait; // Wait before the next attack
        }

        _attackCoroutine = null;
    }

    protected void InvokeOnAttack(IDamageable target)
    {
        OnAttack?.Invoke(target);
    }

    public void SubscribeToAttackEvent(System.Action<IDamageable> handler)
    {
        OnAttack += handler;
    }

    public void UnsubscribeFromAttackEvent(System.Action<IDamageable> handler)
    {
        OnAttack -= handler;
    }
}
