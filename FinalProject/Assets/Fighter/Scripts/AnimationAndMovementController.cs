using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    //Variables to store component objects
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    //Hashes of repeated string values for animator booleans and triggers
    int isWalkingHash;
    int isRunningHash;

    //Movement vectors
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    //Movement booleans
    bool isMovementPressed;
    bool isRunPressed;
    
    //Constants
    float groundedGravity = -.05f;
    float gravity = -9.8f;
    float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 3.0f;
    float walkMultiplier = 1.5f;

    //Called when the script is loading before the game starts
    void Awake()
    {
        //Setting up components
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        //Assigning methods to each input event (event listeners)
        // e.g. onMovementInput is called when Move has started
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();

        if (isRunPressed){
            characterController.Move(currentRunMovement * Time.deltaTime);
        } else {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        handleGravity();
        
    }

    void OnEnable() 
    {
        playerInput.CharacterControls.Enable();
    }
    void OnDisable() 
    {
        playerInput.CharacterControls.Disable();
    }

    //Called when there is movement input
    //Passing in information about the movement event 'context'
    void onMovementInput(InputAction.CallbackContext context) {
        //Reading the input from WASD or joystick
        currentMovementInput = context.ReadValue<Vector2>();

        //Setting x and z values of the player to the new input value multiplied by a run and walk multiplier for different speeds
        currentMovement.x = currentMovementInput.x * walkMultiplier;
        currentMovement.z = currentMovementInput.y * walkMultiplier;
        currentRunMovement.x = currentMovementInput.x * runMultiplier; //run speed multiplier
        currentRunMovement.z = currentMovementInput.y * runMultiplier;

        //Changing movement pressed bool to true if the input wasn't 0
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    //Called when there is input from the run buttons
    //Passing in information about the current movement
    void onRun(InputAction.CallbackContext context){
        //Run is now true if the button has been pressed
        isRunPressed = context.ReadValueAsButton();
    }

    //Called every frame to determine what animation should be playing
    void handleAnimation(){
        //Getting current value of the animation bools from the animator component
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        //If not currently walking and movement has been pressed, set to walking
        if (isMovementPressed && !isWalking){
            animator.SetBool(isWalkingHash, true);
        }
        //If currently moving, but movement is not being pressed, set to not walking
        else if (!isMovementPressed && isWalking){
            animator.SetBool(isWalkingHash, false);
        }

        //If walking and run are being pressed but not currently running, set to running
        if ((isMovementPressed && isRunPressed) && !isRunning){
            animator.SetBool(isRunningHash, true);
        }
        //If currently running but run or walk is not being pressed anymore, set to not running
        else if((!isMovementPressed || !isRunPressed) && isRunning){
            animator.SetBool(isRunningHash, false);
        }
    }

    //Called every frame to make sure the player is rotated in the direction it is moving in
    void handleRotation(){
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if(isMovementPressed){
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    //Called every frame to determine gravity value for the player
    void handleGravity(){
        //Different gravity depending on whether the player is on the ground or not
        //We don't need unecessary extra acceleration when the player is on the ground
        if (characterController.isGrounded){
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        } else {
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }
}
