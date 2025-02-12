using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : PoolableObject
{
    private float _autoDestroyTime = 5f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private int _damage = 10;
    private Rigidbody _rigidBody;

    private const string DISABLE_METHOD_NAME = "Disable";

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, _autoDestroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;

        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(_damage);
        }

        Disable();
    }

    private void Disable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        _rigidBody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
