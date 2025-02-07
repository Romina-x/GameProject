using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float elapsedTime = 0f; // Total elapsed time in seconds

    void Update()
    {
        // Increment elapsed time
        elapsedTime += Time.deltaTime;

        // Convert elapsed time to minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Update the TMP text element
        timerText.text = $"{minutes:00}:{seconds:00}"; // Format as MM:SS
    }
}
