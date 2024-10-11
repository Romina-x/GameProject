using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    // Enum to define different player states
    enum PlayerState
    {
        Idle, Walking, Running, Attacking, Jumping
    }

    // Track current and previous state
    PlayerState currentState = PlayerState.Idle;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Input handling logic for state transitions
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        // Check for jump and attack inputs first (they have priority over movement)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(PlayerState.Jumping);
            return; // Jump has priority, exit to prevent conflicting transitions
        }
        if (Input.GetMouseButtonDown(0))
        {
            ChangeState(PlayerState.Attacking);
            return; // Attack has priority, exit to prevent conflicting transitions
        }

        // Handle state transitions for movement (Walking, Running, Idle)
        if (forwardPressed && runPressed)
        {
            ChangeState(PlayerState.Running); // Run if both forward and run keys are pressed
        }
        else if (forwardPressed)
        {
            ChangeState(PlayerState.Walking); // Walk if only forward is pressed
        }
        else
        {
            ChangeState(PlayerState.Idle); // Otherwise, Idle
        }
    }

    void ChangeState(PlayerState newState)
    {
        if (currentState == newState) return; // Don't transition to the same state

        currentState = newState;

        // Update animator parameters based on the new state
        switch (newState)
        {
            case PlayerState.Idle:
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                break;

            case PlayerState.Walking:
                animator.SetBool("isWalking", true);
                //animator.SetBool("isRunning", false);
                break;

            case PlayerState.Running:
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
                break;

            case PlayerState.Attacking:
                animator.SetTrigger("Attack");
                // Stay in attacking state momentarily, then revert to Idle, Walking, or Running
                break;

            case PlayerState.Jumping:
                animator.SetTrigger("Jump");
                // Stay in jumping state momentarily, then revert to Idle, Walking, or Running
                break;
        }
    }
}
