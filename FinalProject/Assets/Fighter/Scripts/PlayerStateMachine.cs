using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //Variables to store component objects
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    //Hashes of repeated string values for animator booleans and triggers
    int isWalkingHash;
    int isRunningHash;
    int isAttackingHash;
    int isJumpingHash;

    //Movement vectors
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    //Movement booleans
    bool isMovementPressed;
    bool isRunPressed;
    bool isAttackPressed;
    bool isJumpPressed = false;
    
    //Constants
    float groundedGravity = -.05f;
    float gravity = -9.8f;
    float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 3.0f;
    float walkMultiplier = 1.5f;

    //Jumping variables
    float initialJumpVelocity;
    float maxJumpHeight = 1.0f;
    float maxJumpTime = 0.5f;
    bool isJumping = false;
    bool requireNewJumpPress = false;

    // State variables
    PlayerBaseState currentState;
    PlayerStateFactory states;

    // Getters and setters
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public bool IsJumpPressed { get { return isJumpPressed; } }
    public int IsJumpingHash { get { return isJumpingHash; } }
    public bool RequireNewJumpPress { get { return requireNewJumpPress; } set { requireNewJumpPress = value; } }
    public bool IsJumping { set { isJumping = value; } }
    public float CurrentMovementY { get { return currentMovement.y; } set { currentMovement.y = value; } }
    public float CurrentRunMovementY { get { return currentRunMovement.y; } set { currentRunMovement.y = value; } }
    public Animator Animator { get { return animator; } }
    public float InitialJumpVelocity { get { return initialJumpVelocity; } }
    public float GroundedGravity { get { return groundedGravity; } }
    public float Gravity { get { return gravity; } }
    public CharacterController CharacterController { get { return characterController; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public bool IsRunPressed { get { return isRunPressed; } }
    public bool IsAttackPressed { get { return isAttackPressed; } }
    public float CurrentMovementX { set { currentMovement.x = value; } }
    public float CurrentMovementZ { set { currentMovement.z = value; } }
    public float CurrentMovementInputX { get { return currentMovementInput.x; } }
    public float CurrentMovementInputY { get { return currentMovementInput.y; } }
    public float WalkMultiplier { get { return walkMultiplier; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public int IsRunningHash { get { return isRunningHash; } }
    public int IsWalkingHash { get { return isWalkingHash; } }
    public int IsAttackingHash { get { return isAttackingHash; } }
    public float CurrentRunMovementX { set { currentRunMovement.x = value; } }
    public float CurrentRunMovementZ { set { currentRunMovement.z = value; } }

    //Called when the script is loading before the game starts
    void Awake()
    {
        //Setting up components
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Setup state
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isJumpingHash = Animator.StringToHash("isJumping");

        //Assigning methods to each input event (event listeners)
        // e.g. onMovementInput is called when Move has started
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Attack.started += onAttack;
        playerInput.CharacterControls.Attack.canceled += onAttack;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        setupJumpVariables();
    }

    void Update(){
        handleRotation();
        currentState.UpdateStates();
        if (isRunPressed){
            characterController.Move(currentRunMovement * Time.deltaTime);
        } else {
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }

    //Called when there is movement input
    //Passing in information about the movement event 'context'
    void onMovementInput(InputAction.CallbackContext context) {
        //Reading the input from WASD or joystick
        currentMovementInput = context.ReadValue<Vector2>();

        //Setting x and z values of the player to the new input value multiplied by a run and walk multiplier for different speeds
        // currentMovement.x = currentMovementInput.x * walkMultiplier;
        // currentMovement.z = currentMovementInput.y * walkMultiplier;
        // currentRunMovement.x = currentMovementInput.x * runMultiplier; //run speed multiplier
        // currentRunMovement.z = currentMovementInput.y * runMultiplier;

        //Changing movement pressed bool to true if the input wasn't 0
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    //Called when there is input from the run buttons
    //Passing in information about the current movement
    void onRun(InputAction.CallbackContext context){
        //Run is now true if the button has been pressed
        isRunPressed = context.ReadValueAsButton();
    }

    void onAttack(InputAction.CallbackContext context){
        isAttackPressed = context.ReadValueAsButton();
    }

    void onJump(InputAction.CallbackContext context){
        isJumpPressed = context.ReadValueAsButton();
        requireNewJumpPress = false;
    }
    void setupJumpVariables(){
        float timeToApex = maxJumpTime/2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex,2);
        initialJumpVelocity = (2*maxJumpHeight) / timeToApex;
    }

    //Called every frame to make sure the player is rotated in the direction it is moving in
    void handleRotation(){
        Vector3 positionToLookAt;
        positionToLookAt.y = 0.0f;

        if (isRunPressed) {
            positionToLookAt.x = currentRunMovement.x;
            positionToLookAt.z = currentRunMovement.z;
        } else {
            positionToLookAt.x = currentMovement.x;
            positionToLookAt.z = currentMovement.z;
        }
        Quaternion currentRotation = transform.rotation;

        if(isMovementPressed){
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }
    void OnEnable() 
    {
        playerInput.CharacterControls.Enable();
    }
    void OnDisable() 
    {
        playerInput.CharacterControls.Disable();
    }

}
