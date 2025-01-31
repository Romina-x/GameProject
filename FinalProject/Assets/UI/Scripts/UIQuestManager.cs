using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIManager : MonoBehaviour, IRescueObserver
{
    
    [SerializeField] private GameObject _questBox1; 
    [SerializeField] private GameObject _questBox2; 

    [SerializeField] private GameObject goalVFXPrefab;  
    [SerializeField] private Transform goalTransform;
    [SerializeField] private GameObject goalTrigger; // Reference to the pre-made collider

    private List<Animal> _animals;
    private int _rescuedAnimals = 0;
    public int TotalAnimalsToRescue = 3;
    private bool _allAnimalsRescued = false;
    private GameObject _spawnedVFX;

    private void Awake()
    {
        _animals = new List<Animal>(FindObjectsOfType<Animal>());
        foreach (var animal in _animals)
        {
            animal.RegisterRescueObserver(this);
        }

        // Ensure the goal trigger is disabled initially
        if (goalTrigger != null)
        {
            goalTrigger.SetActive(false);
        }
    }

    public void OnAnimalRescued()
    {
        _rescuedAnimals++;

        if (_rescuedAnimals >= TotalAnimalsToRescue)
        {
            _allAnimalsRescued = true;
            StartCoroutine(TransitionToSecondQuest());
            ActivateGoal();
        }
    }

    private IEnumerator TransitionToSecondQuest()
    {
        _questBox1.GetComponent<Animator>().SetTrigger("Hide");
        yield return new WaitForSeconds(2f);
        _questBox1.SetActive(false);
        _questBox2.SetActive(true);
        _questBox2.GetComponent<Animator>().SetTrigger("Show");
    }

    private void ActivateGoal()
    {
        if (goalVFXPrefab != null && goalTransform != null)
        {
            _spawnedVFX = Instantiate(goalVFXPrefab, goalTransform.position, Quaternion.identity);
        }

        if (goalTrigger != null)
        {
            goalTrigger.SetActive(true); // Enable the pre-made collider
            Debug.Log("Goal is now active!");
        }
    }
}
