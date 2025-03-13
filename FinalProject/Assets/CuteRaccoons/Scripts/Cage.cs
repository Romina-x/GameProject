using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages cage that holds trapped animal
// Cage should disappear when associated enemies are defeated
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
        // Subscribe only to the `OnDefeated` event of associated enemies
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
        // Unsubscribe from associated enemies' `OnDefeated` event
        foreach (Enemy enemy in AssociatedEnemies)
        {
            if (enemy != null)
            {
                enemy.OnDefeated -= OnEnemyDefeated;
            }
        }
    }

    private void OnEnemyDefeated(int score)
    {
        _defeatedEnemiesCount++; // Increase defeated enemy count

        if (!_isFreed && _defeatedEnemiesCount >= AssociatedEnemies.Count)
        {
            FreeAnimal();
        }
    }

    private void FreeAnimal()
    {
        if (PoofEffect != null)
        {
            Instantiate(PoofEffect, transform.position, Quaternion.identity);
        }

        SoundFXManager.instance.PlaySoundFX(_releaseClip, transform, 1f);

        if (AssociatedAnimal != null)
        {
            AssociatedAnimal.StartFollowing();
        }

        Destroy(gameObject);
        _isFreed = true;
    }
}
