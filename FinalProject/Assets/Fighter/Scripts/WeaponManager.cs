using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject weapon;
    private void Start()
    {
        // Ensure the weapon collider starts disabled
        if (weapon != null)
        {
            var collider = weapon.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
    public void EnableWeaponCollider(int isEnable){
        if (weapon != null){
            var collider = weapon.GetComponent<Collider>();
            if (collider != null){
                if (isEnable == 1){
                    collider.enabled = true;
                } else{
                    collider.enabled = false;
                }
            }
        }
    }
}
