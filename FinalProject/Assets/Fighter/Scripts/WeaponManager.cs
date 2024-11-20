using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the weapon's collider
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

    // Animator event
    // Weapon collider should only be enabled when player is swinging their weapon
    // isEnable is 1 when the attack animation is happening
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
