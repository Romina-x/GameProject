using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the enemy attack radius
// When player is close enough (in this radius) the enemy attacks
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

    // Triggered when something enters the radius
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

    // Triggered when something leaves the radius
    protected virtual void OnTriggerExit(Collider other) 
    {
        _damageable = other.GetComponent<IDamageable>();

        if (_damageable != null)
        {
            _damageable = null;

            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    protected virtual IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(_attackDelay);
        
        // Keep attacking as long as the player is within the radius and the enemy can attack
        while (_damageable != null)
        {
            if (GetComponentInParent<Enemy>().CanAttack())
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
