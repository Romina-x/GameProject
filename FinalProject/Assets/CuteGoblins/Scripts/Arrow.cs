using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represents an arrow projectile that deals damage upon collision.
/// This class extends <see cref="PoolableObject"/> to allow object pooling.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Arrow : PoolableObject
{
    private float _autoDestroyTime = 8f; // Time active before arrow is automatically destroyed
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
        // Call disable after auto destroy time
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, AutoDestroyTime); 
    }

    /// <summary>
    /// Handles collision with other objects.
    /// If the object implements <see cref="IDamageable"/>, it takes damage.
    /// </summary>
    /// <param name="other">The collider of the object the arrow hit.</param>
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;

        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(_damage);
        }

        Disable();
    }

    /// <summary>
    /// Deactivates the arrow and stops its movement.
    /// </summary>
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
