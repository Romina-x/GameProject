using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a cage that holds a trapped animal. 
/// The cage disappears when all associated enemies are defeated.
/// </summary>
public class Cage : MonoBehaviour
{
    public List<Enemy> AssociatedEnemies;
    public GameObject PoofEffect;
    public Animal AssociatedAnimal;

    [SerializeField] private AudioClip _releaseClip;
    
    private bool _isFreed = false;
    private int _defeatedEnemiesCount = 0;

    void Awake()
    {
        // Subscribe to the OnDefeated event of associated enemies
        foreach (Enemy enemy in AssociatedEnemies)
        {
            if (enemy != null)
            {
                enemy.OnDefeated += OnEnemyDefeated;
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from ondefeated event of associated enemies
        foreach (Enemy enemy in AssociatedEnemies)
        {
            if (enemy != null)
            {
                enemy.OnDefeated -= OnEnemyDefeated;
            }
        }
    }

    /// <summary>
    /// Called when an enemy is defeated. If all enemies are defeated, it frees the animal.
    /// </summary>
    private void OnEnemyDefeated(int score)
    {
        _defeatedEnemiesCount++; // Increase defeated enemy count

        if (!_isFreed && _defeatedEnemiesCount >= AssociatedEnemies.Count)
        {
            FreeAnimal();
        }
    }

    /// <summary>
    /// Frees the animal, plays the sound and visual effect for cage release, and destroys the cage object.
    /// </summary>
    private void FreeAnimal()
    {
        if (PoofEffect != null)
        {
            Instantiate(PoofEffect, transform.position, Quaternion.identity);
        }

        SoundFXManager.instance.PlaySoundFX(_releaseClip, transform, 1f);

        if (AssociatedAnimal != null)
        {
            AssociatedAnimal.StartFollowing(); // Get released animal to start following the player
        }

        Destroy(gameObject);
        _isFreed = true;
    }
}
