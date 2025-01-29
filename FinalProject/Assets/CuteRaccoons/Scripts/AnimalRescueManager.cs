using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRescueManager : MonoBehaviour, IRescueObserver
{
    public static AnimalRescueManager Instance;
    private int _rescuedAnimals = 0;
    public int TotalAnimalsToRescue = 3;
    private List<Animal> _animals;

    private void Awake()
    {
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
            Debug.Log("All animals rescued! Head to the goal!");
        }
    }
}

