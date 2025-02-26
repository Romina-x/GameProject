using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenRunner : MonoBehaviour
{
    [SerializeField] private Transform centerPoint; // The point they will run around
    [SerializeField] private float radius = 3f; // Distance from the center
    [SerializeField] private float speed = 2f; // Speed of movement

    private float currentAngle;
    private Animator animator;

    private void Start()
    {
        // Get the Animator component and set it to running state
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", true); // Adjust based on your animation parameters
            animator.SetTrigger("walk");
        }

        // If no center is assigned, use world origin
        if (centerPoint == null)
        {
            Debug.LogWarning($"{gameObject.name} has no center point assigned! Using world origin.");
            centerPoint = new GameObject("DefaultCenterPoint").transform;
            centerPoint.position = Vector3.zero;
        }

        // Calculate the initial angle based on current position
        Vector3 direction = transform.position - centerPoint.position;
        currentAngle = Mathf.Atan2(direction.z, direction.x);
    }

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
