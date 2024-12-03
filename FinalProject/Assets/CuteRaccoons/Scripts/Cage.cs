using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages cage that holds trapped animal
// Cage should disappear when associated enemies are defeated
public class Cage : MonoBehaviour
{
    // Components that are assigned in the unity editor
    public List<Enemy> AssociatedEnemies;
    public GameObject PoofEffect;
    
    private bool _isFreed = false;


    void Awake(){
        Enemy.SubscribeToEnemyDefeated(CheckIfAllEnemiesDefeated);
    }

    // When cage object is destroyed
    private void OnDestroy() {
        Enemy.UnsubscribeFromEnemyDefeated(CheckIfAllEnemiesDefeated);
    }

    // Every time an enemy dies, this method is called
    private void CheckIfAllEnemiesDefeated()
    {
        if (!_isFreed && AreAllEnemiesDefeated())
        {
            FreeAnimal();
        }
    }

    // Check if all associated enemies have been defeated
    private bool AreAllEnemiesDefeated()
    {
        foreach (Enemy enemy in AssociatedEnemies)
        {
            if (enemy != null && !enemy.IsDefeated)
            {
                return false;
            }
        }
        return true;
    }

    // Cage disappears with poof VFX to free animal
    private void FreeAnimal()
    {
        if (PoofEffect != null)
        {
            Instantiate(PoofEffect, transform.position, Quaternion.identity);
        }
        // Destroy this game object
        Destroy(gameObject);
        _isFreed = true;
    }
}
