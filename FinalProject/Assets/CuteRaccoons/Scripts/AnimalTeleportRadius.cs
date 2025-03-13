using UnityEngine;

/// <summary>
/// represents the radius around the player which the animals should be within.
/// Teleports animals back to the player if they exit this radius.
/// </summary>
public class TeleportRadius : MonoBehaviour
{
    private Transform _playerTransform;

    void Awake()
    {
        // Get the player's transform from parent object
        _playerTransform = transform.parent;
    }

    /// <summary>
    /// Called when another collider exits the radius.
    /// If the exiting object is an animal, it will teleport to the player.
    /// </summary>
    /// <param name="other">The collider of the object that exited the radius.</param>
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is an animal
        Animal animal = other.GetComponent<Animal>();
        if (animal != null)
        {
            animal.TeleportToPlayer(); // Trigger the teleport behavior
        }
    }
}

