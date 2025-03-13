using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the weapon's collider, ensuring it is only active at the correct time during the attack animation.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    // Weapon assigned in the unity editor
    public GameObject Weapon;

    private void Start()
    {
        // Ensure the weapon collider starts disabled
        if (Weapon != null)
        {
            var collider = Weapon.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    /// <summary>
    /// Enables or disables the weapon's collider based on the attack animation state.
    /// This method is called by an animation event.
    /// </summary>
    /// <param name="isEnable">1 to enable the collider, 0 to disable it.</param>
    public void EnableWeaponCollider(int isEnable)
    {
        if (Weapon != null)
        {
            var collider = Weapon.GetComponent<Collider>();
            if (collider != null)
            {
                if (isEnable == 1)
                {
                    collider.enabled = true;
                } 
                else
                {
                    collider.enabled = false;
                }
            }
        }
    }
}
