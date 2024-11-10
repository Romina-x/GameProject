using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    [SerializeField]
    private int damage = 20;

    private void OnTriggerEnter(Collider other){
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(damageable != null){
            damageable.TakeDamage(damage);
        }
    }
}
