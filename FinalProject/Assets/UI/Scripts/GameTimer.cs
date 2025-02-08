using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    private float _elapsedTime = 0f; // Total elapsed time in seconds

    public float ElapsedTime => _elapsedTime;

    void Update()
    {
        // Increment elapsed time
        _elapsedTime += Time.deltaTime;

        // Convert elapsed time to minutes and seconds
        int minutes = Mathf.FloorToInt(_elapsedTime / 60);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);

        // Update the TMP text element
        _timerText.text = $"{minutes:00}:{seconds:00}"; // Format as MM:SS
    }
}
