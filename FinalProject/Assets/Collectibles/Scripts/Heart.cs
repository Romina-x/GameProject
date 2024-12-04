using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private int _heartHealth = 50; // Amount of health the heart restores
    [SerializeField] private GameObject collectionVFX; // VFX for collection

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthAndDamage playerHealth = other.GetComponent<PlayerHealthAndDamage>();

        if (playerHealth != null)
        {
            // Update the player's health
            playerHealth.AddHealth(_heartHealth);
            
            // Play collection VFX
            if (collectionVFX != null)
            {
                Instantiate(collectionVFX, transform.position, Quaternion.identity);
            }

            // Remove the heart
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Heart destroyed: ");
    }
}
