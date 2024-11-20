using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines strength of player's weapon
public class WeaponAttributes : MonoBehaviour
{
    // Weapon damage, can be changed from the unity editor
    [SerializeField]
    private int damage = 20;

    // Called when weapon hitbox collides with an enemy
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(damageable != null)
        {
            // Call takedamage method on enemy gameobject
            damageable.TakeDamage(damage);
        }
    }
}
