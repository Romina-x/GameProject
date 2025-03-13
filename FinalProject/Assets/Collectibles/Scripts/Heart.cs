using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the functionality of the heart object that gives the player health when collected.
/// </summary>
public class Heart : MonoBehaviour
{
    [SerializeField] private int _heartHealth = 50; // Amount of health the heart restores
    [SerializeField] private GameObject collectionVFX; // Virtual FX for heart collection
    [SerializeField] private AudioClip _collectClip; // Sound FX for heart collection


    /// <summary>
    /// Called when another entity collides with the heart's collider.
    /// If the other collider is the player, it will restore health to the player.
    /// </summary>
    /// <param name="other">The collider of the object that entered the heart's collider.</param>
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
