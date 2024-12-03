using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private int _heartHealth = 50; // Amount of health the heart restores

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthAndDamage playerHealth = other.GetComponent<PlayerHealthAndDamage>();
        if (playerHealth != null)
        {
            playerHealth.AddHealth(_heartHealth);
            Destroy(gameObject);
        }
    }
}
