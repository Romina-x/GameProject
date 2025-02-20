using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : PoolableObject
{
    private float _autoDestroyTime = 8f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private int _damage = 10;
    public Rigidbody Rigidbody;

    private const string DISABLE_METHOD_NAME = "Disable";

    // Properties
    public int Damage { get { return _damage; } set { _damage = value; } }

    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }


    public float AutoDestroyTime { get { return _autoDestroyTime; } set { _autoDestroyTime = value; } }


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, AutoDestroyTime);
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
        if (Parent == null)
        {
            Debug.LogError($"{gameObject.name} has no Parent assigned!");
        }
        CancelInvoke(DISABLE_METHOD_NAME);
        Rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
