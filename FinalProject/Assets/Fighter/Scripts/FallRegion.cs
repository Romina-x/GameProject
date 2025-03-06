using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRegion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthAndDamage playerHealth = other.GetComponent<PlayerHealthAndDamage>();
        if (playerHealth != null)
        {
            // Kill the player if they have fallen off the map
            playerHealth.TakeDamage(300); 
        }
    }

}
