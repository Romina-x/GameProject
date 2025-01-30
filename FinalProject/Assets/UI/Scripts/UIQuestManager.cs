using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIManager : MonoBehaviour, IRescueObserver
{
    [SerializeField] private GameObject _questBox1; // "Find the animals!" 
    [SerializeField] private GameObject _questBox2; // "Reach the goal!" 
    private Animator _quest1Animator;
    private Animator _quest2Animator;


    private List<Animal> _animals; 
    private int _rescuedAnimals = 0;
    public int TotalAnimalsToRescue = 3;
    private bool _allAnimalsRescued = false;

    private void Awake()
    {
        _quest1Animator = _questBox1.GetComponent<Animator>();
        _quest2Animator = _questBox2.GetComponent<Animator>();

        // Find all animals in the scene and register as observer
        _animals = new List<Animal>(FindObjectsOfType<Animal>());
        foreach (var animal in _animals)
        {
            animal.RegisterRescueObserver(this);
        }
    }

    public void OnAnimalRescued()
    {
        _rescuedAnimals++;
        Debug.Log($"Animals Rescued: {_rescuedAnimals}/{TotalAnimalsToRescue}");

        if (_rescuedAnimals >= TotalAnimalsToRescue)
        {
            _allAnimalsRescued = true;
            Debug.Log("All animals rescued! Head to the goal!");
            StartCoroutine(TransitionToSecondQuest());
        }

        
    }

    private IEnumerator TransitionToSecondQuest()
    {
        // Play first quest's "hide" animation
        _quest1Animator.SetTrigger("Hide");
        
        // Wait for the animation to finish
        yield return new WaitForSeconds(2f); // Adjust based on animation length

        // Disable the orignal quest
        _questBox1.SetActive(false);

        // Play second quest's "show" animation
        _questBox2.SetActive(true);
    }
}

