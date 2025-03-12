using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private int _heartHealth = 50; // Amount of health the heart restores
    [SerializeField] private GameObject collectionVFX; // VFX for collection
    [SerializeField] private AudioClip _collectClip;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthAndDamage playerHealth = other.GetComponent<PlayerHealthAndDamage>();

        if (playerHealth != null)
        {
            // Update the player's health
            playerHealth.AddHealth(_heartHealth);

            // Play collection VFX & Sound effect
            if (collectionVFX != null)
            {
                Instantiate(collectionVFX, transform.position, Quaternion.identity);
            }
            SoundFXManager.instance.PlaySoundFX(_collectClip, transform, 1f);

            // Remove the heart
            Destroy(gameObject);
        }
    }
}
