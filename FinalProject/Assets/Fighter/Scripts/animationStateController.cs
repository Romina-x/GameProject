using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if (!isWalking && forwardPressed){
            animator.SetBool("isWalking", true);
        }
        if (isWalking && !forwardPressed){
            animator.SetBool("isWalking", false);
        }

        if (!isRunning && forwardPressed && runPressed)
        {
            animator.SetBool("isRunning", true);
        }
        else if (isRunning && (!forwardPressed || !runPressed))
        {
            // Stop running and either go back to walking or idle
            animator.SetBool("isRunning", false);

            // If forward key is still pressed, continue walking
            if (forwardPressed)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                // If no forward key is pressed, stop walking as well
                animator.SetBool("isWalking", false);
            }
        }
    }
}
