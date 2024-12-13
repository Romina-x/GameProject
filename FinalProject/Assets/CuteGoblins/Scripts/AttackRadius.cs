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

    // List of damageable game objects in the radius
    private List<IDamageable> _damageables = new List<IDamageable>();

    // Attack settings
    private float _attackDelay = 0.5f;
    private int _damage = 10;

    // Attack event
    public event Action<IDamageable> OnAttack;
    private Coroutine _attackCoroutine;

    // Properties
    public float AttackDelay { set { _attackDelay = value; } }
    public int Damage { set { _damage = value; } }

    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }

    // Triggered when something enters the radius
    private void OnTriggerEnter(Collider other) 
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        // If object is a damageable
        if (damageable != null)
        {
            _damageables.Add(damageable);
            // Begin attacking
            if(_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    // Triggered when something leaves the radius
    private void OnTriggerExit(Collider other) 
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            _damageables.Remove(damageable);
            // Stop attacking
            if(_damageables.Count == 0){
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    private IEnumerator Attack()
    {
        // Delay between attacks
        WaitForSeconds wait = new WaitForSeconds(_attackDelay);
        yield return wait;

        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;
        while (_damageables.Count > 0)
        {
            // Determine closest damageable
            for (int i = 0; i < _damageables.Count; i++)
            {
                Transform damageableTransform = _damageables[i].GetTransform();
                float distance = Vector3.Distance(transform.position, damageableTransform.position);

                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamageable = _damageables[i];
                }
            }

            // Call TakeDamage on damageable and invoke OnAttack event 
            if(closestDamageable != null)
            {
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.TakeDamage(_damage);
            }

            // Reset closest
            closestDamageable = null;
            closestDistance = float.MaxValue;

            // Wait between each attack for the set delay
            yield return wait;
            _damageables.RemoveAll(DisabledDamageables);
        }

        _attackCoroutine = null;
    }

    public void StopAttackCoroutine()
    {
        if (_attackCoroutine != null) 
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
            Collider.enabled = false;
        }
    } 

    private bool DisabledDamageables(IDamageable damageable)
    {
        return damageable != null && !damageable.GetTransform().gameObject.activeSelf;
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
