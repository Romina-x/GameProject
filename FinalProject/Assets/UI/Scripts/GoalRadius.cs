using UnityEngine;

public class GoalArea : MonoBehaviour
{
    public LevelCleared levelCleared;

    // Detects when the player enters the goal area
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        // Notify the LevelCleared script to call Setup()
        levelCleared.Setup();
    }
}
