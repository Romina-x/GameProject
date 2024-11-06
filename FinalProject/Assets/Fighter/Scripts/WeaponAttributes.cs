using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    [SerializeField]
    private int damage = 20;

    private void OnTriggerEnter(Collider other){
        Debug.Log("ontriggerenter");
        IDamageable damageable = other.GetComponent<IDamageable>();
        Debug.Log("ontriggerenter" + damageable.GetName());
        if(damageable != null){
            damageable.TakeDamage(damage);
        }
    }
}
