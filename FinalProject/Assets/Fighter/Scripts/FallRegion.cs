using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to manage player death from falling off the map.
/// This collider determines the point that the player dies once they fall off.
/// </summary>
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
