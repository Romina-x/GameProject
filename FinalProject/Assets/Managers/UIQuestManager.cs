using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUIManager : MonoBehaviour, IRescueObserver
{
    
    [SerializeField] private GameObject _questBox1; 
    [SerializeField] private GameObject _questBox2; 

    [SerializeField] private GameObject _goalVFXPrefab;  
    [SerializeField] private Transform _goalTransform;
    [SerializeField] private GameObject _goalRadius; 
    [SerializeField] private GameObject _goalIndicator;

    [SerializeField] private TextMeshProUGUI _rescuedAnimalsText;

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

        // Ensure the goal trigger and indicator are disabled initially
        if (_goalRadius != null)
        {
            _goalRadius.SetActive(false);
        }

        if (_goalIndicator != null)
        {
            _goalIndicator.SetActive(false);
        }
        
        _rescuedAnimalsText.text = $"{_rescuedAnimals}/{TotalAnimalsToRescue}"; // Update the HUD display
    }

    public void OnAnimalRescued()
    {
        _rescuedAnimals++;
        _rescuedAnimalsText.text = $"{_rescuedAnimals}/{TotalAnimalsToRescue}"; // Update the HUD display

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
        yield return new WaitForSeconds(2.5f);
        _questBox1.SetActive(false);
        
        _questBox2.SetActive(true);
        _questBox2.GetComponent<Animator>().SetTrigger("Show");
    }

    private void ActivateGoal()
    {
        // Instantiate the goal VFX
        if (_goalVFXPrefab != null && _goalTransform != null)
        {
            _spawnedVFX = Instantiate(_goalVFXPrefab, _goalTransform.position, Quaternion.identity);
        }

        // Enable the goal collider
        if (_goalRadius != null)
        {
            _goalRadius.SetActive(true); // Enable the pre-made collider
        }

        // Enable the goal indicator
        _goalIndicator.SetActive(true);
    }
}
