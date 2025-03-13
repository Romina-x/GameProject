using System.Collections;
using UnityEngine;
using TMPro;


/// <summary>
/// Manages the UI for the game's quest system, tracking rescued animals and unlocking the goal.
/// </summary>
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
        Animal.OnAnimalRescued += OnAnimalRescued; // Subscribe to animal rescued event
    }

    private void OnDisable()
    {
        Animal.OnAnimalRescued -= OnAnimalRescued; // Unsubscribe to animal rescued event
    }

    private void Awake()
    {
        _rescuedAnimalsText.text = $"{_rescuedAnimals}/{TotalAnimalsToRescue}"; // Change UI to display number of animals to rescue
        if (_goalRadius != null) _goalRadius.SetActive(false); // Disable goal
    }

    /// <summary>
    /// Called when an animal is rescued, updating the UI and checking if the quest is completed.
    /// </summary>
    private void OnAnimalRescued()
    {
        _rescuedAnimals++;
        _rescuedAnimalsText.text = $"{_rescuedAnimals}/{TotalAnimalsToRescue}"; // Update UI

        // If all animals have been rescued, show new quest and activate the goal
        if (_rescuedAnimals >= TotalAnimalsToRescue && !_allAnimalsRescued)
        {
            _allAnimalsRescued = true;
            StartCoroutine(TransitionToSecondQuest());
            ActivateGoal();
        }
    }

    private IEnumerator TransitionToSecondQuest()
    {
        // Animate quest box transition to new quest
        _questBox1.GetComponent<Animator>().SetTrigger("Hide");
        yield return new WaitForSeconds(2.5f);
        _questBox1.SetActive(false);

        _questBox2.SetActive(true);
        _questBox2.GetComponent<Animator>().SetTrigger("Show");
    }

    private void ActivateGoal()
    {
        // Spawn goal VFX and enable the goal collider
        if (_goalVFXPrefab != null && _goalTransform != null)
            _spawnedVFX = Instantiate(_goalVFXPrefab, _goalTransform.position, Quaternion.identity);

        if (_goalRadius != null) _goalRadius.SetActive(true);
    }
}
