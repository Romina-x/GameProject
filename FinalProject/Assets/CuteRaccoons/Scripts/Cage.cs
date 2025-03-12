using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages cage that holds trapped animal
// Cage should disappear when associated enemies are defeated
public class Cage : MonoBehaviour, IDefeatObserver
{
    // Components that are assigned in the unity editor
    public List<Enemy> AssociatedEnemies;
    public GameObject PoofEffect;
    public Animal AssociatedAnimal;
    
    [SerializeField] private AudioClip _releaseClip;
    
    private bool _isFreed = false;


    void Awake(){
        // Register this cage as an observer of all it's associated enemies
        foreach (var enemy in AssociatedEnemies)
        {
            if (enemy != null)
            {
                enemy.RegisterDefeatObserver(this);
            }
        }
    }

    // When cage object is destroyed
    private void OnDestroy() {
        // Unregister from all enemies
        foreach (var enemy in AssociatedEnemies)
        {
            if (enemy != null)
            {
                enemy.UnregisterDefeatObserver(this);
            }
        }
    }

    // Cage is notified when an associated enemy dies and subsequently checks if all are dead
    public void OnNotify(int score)
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

        SoundFXManager.instance.PlaySoundFX(_releaseClip, transform, 1f);

        // Tell the freed animal to start following the player
        if (AssociatedAnimal != null)
        {
            AssociatedAnimal.StartFollowing();
        }

        // Destroy this game object
        Destroy(gameObject);
        _isFreed = true;
    }
}
