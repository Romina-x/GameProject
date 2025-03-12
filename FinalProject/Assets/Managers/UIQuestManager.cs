using System.Collections;
using UnityEngine;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _questBox1;
    [SerializeField] private GameObject _questBox2;
    [SerializeField] private GameObject _goalVFXPrefab;
    [SerializeField] private Transform _goalTransform;
    [SerializeField] private GameObject _goalRadius;
    [SerializeField] private TextMeshProUGUI _rescuedAnimalsText;

    private int _rescuedAnimals = 0;
    public int TotalAnimalsToRescue = 3;
    private bool _allAnimalsRescued = false;
    private GameObject _spawnedVFX;

    private void OnEnable()
    {
        Animal.OnAnimalRescued += OnAnimalRescued; // Subscribe to event
    }

    private void OnDisable()
    {
        Animal.OnAnimalRescued -= OnAnimalRescued; // Unsubscribe to prevent memory leaks
    }

    private void Awake()
    {
        _rescuedAnimalsText.text = $"{_rescuedAnimals}/{TotalAnimalsToRescue}"; // Initialize UI
        if (_goalRadius != null) _goalRadius.SetActive(false); // Hide goal at start
    }

    private void OnAnimalRescued()
    {
        _rescuedAnimals++;
        _rescuedAnimalsText.text = $"{_rescuedAnimals}/{TotalAnimalsToRescue}"; 

        if (_rescuedAnimals >= TotalAnimalsToRescue && !_allAnimalsRescued)
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
        if (_goalVFXPrefab != null && _goalTransform != null)
            _spawnedVFX = Instantiate(_goalVFXPrefab, _goalTransform.position, Quaternion.identity);

        if (_goalRadius != null) _goalRadius.SetActive(true);
    }
}
