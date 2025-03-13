using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the circular movement and animation of the player/animals for the title screen.
/// </summary>
public class TitleMovement : MonoBehaviour
{
    [SerializeField] private Transform centerPoint; 
    [SerializeField] private float radius = 3f; 
    [SerializeField] private float speed = 2f; // Speed of movement
    private float currentAngle;

    private Animator animator;

    private void Start()
    {
        // Get the Animator component and set it to running state
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", true); //
            animator.SetBool("isRunning", true); // Player
            animator.SetTrigger("walk"); // Raccoon
        }

        // Calculate the initial angle based on current position
        Vector3 direction = transform.position - centerPoint.position;
        currentAngle = Mathf.Atan2(direction.z, direction.x);
    }

    /// <summary>
    /// Updates the object's position and rotation to move in a circular path.
    /// </summary>
    private void Update()
    {
        // Update angle over time
        currentAngle += speed * Time.deltaTime;

        // Calculate new position in the circular path
        float x = centerPoint.position.x + Mathf.Cos(currentAngle) * radius;
        float z = centerPoint.position.z + Mathf.Sin(currentAngle) * radius;

        transform.position = new Vector3(x, transform.position.y, z);

        // Rotate to face movement direction
        Vector3 nextPosition = new Vector3(
            centerPoint.position.x + Mathf.Cos(currentAngle + 0.1f) * radius,
            transform.position.y,
            centerPoint.position.z + Mathf.Sin(currentAngle + 0.1f) * radius
        );

        transform.LookAt(nextPosition);
    }
}
