using UnityEngine;

public class TeleportRadius : MonoBehaviour
{
    private Transform _playerTransform; 

    void Awake()
    {
        // Get the player's transform from parent object
        _playerTransform = transform.parent;
    }
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

