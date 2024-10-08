using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Existing walking/running logic
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        // Walking logic
        if (!isWalking && forwardPressed)
        {
            animator.SetBool("isWalking", true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool("isWalking", false);
        }

        // Running logic
        if (!isRunning && forwardPressed && runPressed)
        {
            animator.SetBool("isRunning", true);
        }
        else if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool("isRunning", false);
            if (forwardPressed)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }


        // Handle attack animation
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        // Handle jump animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

    }
}
