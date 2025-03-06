using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackRadius : AttackRadius
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Arrow _arrowPrefab;
    private Vector3 _arrowSpawnOffset = new Vector3(0.27f, 0.25f , 0);
    [SerializeField] private LayerMask Mask;
    [SerializeField] private float _arrowSpawnDelay;
    private ObjectPool _arrowPool;
    

    [SerializeField] private float SpherecastRadius = 0.1f;
    private RaycastHit _hit;
    private IDamageable _target;
    private Arrow _arrow;

    protected override void Awake()
    {
        base.Awake();
        {
            _arrowPool = ObjectPool.CreateInstance(_arrowPrefab, Mathf.CeilToInt((1 / _attackDelay) * _arrowPrefab.AutoDestroyTime));
        }
    }

    protected override IEnumerator Attack()
    {
        WaitForSeconds Wait = new WaitForSeconds(_attackDelay);

        yield return Wait;

        while (_damageable != null)
        {
            
            Debug.Log("While");
            
            if (HasLineOfSightTo(_damageable.GetTransform()))
            {
                _agent.isStopped = true;
                Debug.Log("Invoking Onattack");

                if (_damageable != null)
                {

                    PoolableObject poolableObject = _arrowPool.GetObject();
                    if (poolableObject != null)
                    {


                        _arrow = poolableObject.GetComponent<Arrow>();

                        _arrow.Damage = _damage;
                        //_arrow.transform.position = transform.position + _arrowSpawnOffset;
                        _arrow.transform.position = transform.TransformPoint(_arrowSpawnOffset);

                        _arrow.transform.rotation = _agent.transform.rotation;
                        _arrow.Rigidbody.AddForce(_agent.transform.forward * _arrowPrefab.MoveSpeed, ForceMode.VelocityChange);
                        InvokeOnAttack(_damageable);
                    }
                }
                else
                {
                    _agent.isStopped = false;
                }
            }


            yield return Wait;

            if (_damageable == null || !HasLineOfSightTo(_damageable.GetTransform()))
            {
                _agent.isStopped = false;
            }
        }

        _agent.isStopped = false;
        _attackCoroutine = null;

    }

    private bool HasLineOfSightTo(Transform target)
    {
        if (Physics.SphereCast(transform.position + _arrowSpawnOffset, SpherecastRadius, ((target.position + _arrowSpawnOffset) - (transform.position + _arrowSpawnOffset)).normalized, out _hit, Collider.radius, Mask))
        {
            IDamageable damageable;
            if (_hit.collider.TryGetComponent<IDamageable>(out damageable))
            {
                return damageable.GetTransform() == target;
            }
        }

        return false;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (_attackCoroutine == null)
        {
            _agent.enabled = true;
        }
    }

}
