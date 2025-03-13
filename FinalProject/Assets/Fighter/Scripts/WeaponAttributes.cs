using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the weapon collider and defines the strength of the player's weapon.
/// </summary>
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
            // Call takedamage method on enemy
            damageable.TakeDamage(damage);
        }
    }
}
