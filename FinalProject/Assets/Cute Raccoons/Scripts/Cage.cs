using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public List<Enemy> associatedEnemies;
    public GameObject poofEffect;
    private bool isFreed = false;


    void Awake(){
        Enemy.OnEnemyDefeated += CheckIfAllEnemiesDefeated;
    }

    private void OnDestroy() {
        Enemy.OnEnemyDefeated -= CheckIfAllEnemiesDefeated;
    }

    private void CheckIfAllEnemiesDefeated(){
        if (!isFreed && AreAllEnemiesDefeated()){
            FreeAnimal();
        }
    }

    private bool AreAllEnemiesDefeated(){
        foreach (Enemy enemy in associatedEnemies){
            if (enemy != null && !enemy.IsDefeated){
                return false;
            }
        }
        return true;
    }

    private void FreeAnimal(){
        if (poofEffect != null){
            Instantiate(poofEffect, transform.position, Quaternion.identity);

        }
        Destroy(gameObject);
        isFreed = true;
    }
}
