using UnityEngine;

/// <summary>
/// Represents the goal collider in the level.
/// Triggers level completion when the player enters the area.
/// </summary>
public class GoalRadius : MonoBehaviour
{
    public LevelCleared levelCleared;

    // Detects when the player enters the goal radius
    private void OnTriggerEnter(Collider other)
    {
        // Notify the LevelCleared script to call Setup()
        levelCleared.Setup();
    }
}
