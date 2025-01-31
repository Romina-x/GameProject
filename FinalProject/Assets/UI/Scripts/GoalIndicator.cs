using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalIndicator : MonoBehaviour
{
    public Transform player;
    public Transform goal;    
    public Camera mainCamera; 
    public GameObject arrow;

    private RectTransform arrowRect;

    void Start()
    {
        arrowRect = arrow.GetComponent<RectTransform>(); // Get the UI transform of the arrow
    }

    // Updating the rotation and position of the arrow every frame relative to the goal position
    void Update()
    {
        // Convert the goal's position from 3D space to 2D space
        Vector3 goalScreenPos = mainCamera.WorldToScreenPoint(goal.position);

        // Calculate whether the goal's position is within the boundaties of the screen
        bool isGoalOnScreen = goalScreenPos.z > 0 && 
                            goalScreenPos.x > 0 && goalScreenPos.x < Screen.width &&
                            goalScreenPos.y > 0 && goalScreenPos.y < Screen.height;

        // Deactivate the arrow if the goal is off screen & return
        if (isGoalOnScreen)
        {
            arrow.SetActive(false); 
            return;
        }

        arrow.SetActive(true); // The goal is off screen so activate the arrow

        // Goal is off-screen, move arrow to screen edge
        Vector3 direction = (goal.position - player.position).normalized;
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 boundedPosition = screenCenter + new Vector3(direction.x, direction.z, 0) * (Screen.height / 2 - 10); 

        // Apply padding by clamping position considering screen bounds and padding
        boundedPosition.y = Mathf.Clamp(boundedPosition.y, 100, Screen.height - 100);
        boundedPosition.x = Mathf.Clamp(boundedPosition.x, 10, Screen.width - 10);

        // Set the arrow's new position
        arrowRect.position = clampedPosition;

        // Rotate the arrow to point toward the goal
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        arrowRect.rotation = Quaternion.Euler(0, 0, angle);
    }
}
