using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    public SphereCollider collider;
    private List<IDamageable> damageables = new List<IDamageable>();
    public int damage = 10;
    public float attackDelay = 0.5f;
    public delegate void AttackEvent(IDamageable target);
    public AttackEvent OnAttack;
    private Coroutine attackCoroutine;

    private void Awake(){
        collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other) {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null){
            damageables.Add(damageable);
            if(attackCoroutine == null){
                attackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null){
            damageables.Remove(damageable);
            if(damageables.Count == 0){
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    private IEnumerator Attack(){
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        yield return wait;
        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;
        while (damageables.Count > 0){
            for (int i = 0; i < damageables.Count; i++){
                Transform damageableTransform = damageables[i].GetTransform();
                float distance = Vector3.Distance(transform.position, damageableTransform.position);

                if(distance < closestDistance){
                    closestDistance = distance;
                    closestDamageable = damageables[i];
                }
            }
            if(closestDamageable != null){
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.TakeDamage(damage);
            }

            closestDamageable = null;
            closestDistance = float.MaxValue;

            yield return wait;
            damageables.RemoveAll(DisabledDamageables);
        }

        attackCoroutine = null;
    }
    public IDamageable GetClosestTarget()
    {
        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;
        foreach (var damageable in damageables)
        {
            if (damageable == null) continue;

            float distance = Vector3.Distance(transform.position, damageable.GetTransform().position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDamageable = damageable;
            }
        }
        return closestDamageable;
    }
    private bool DisabledDamageables(IDamageable damageable){
        return damageable == null;
    }
}
