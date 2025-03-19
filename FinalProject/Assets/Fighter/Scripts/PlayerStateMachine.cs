using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

/// <summary>
/// State machine to manage transitions between player movement states based on inputs.
/// </summary>
public class PlayerStateMachine : MonoBehaviour
{
    // Player components
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private Animator _animator;
    private NavMeshAgent _agent;

    // Input vectors
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _currentRunMovement;

    // Input state booleans
    private bool _isMovementPressed;
    private bool _isRunPressed;
    private bool _isJumpPressed = false;

    // Jump logic
    private bool _isJumping = false;
    private bool _requireNewJumpPress = false;

    // Movement physics constants
    private readonly float _groundedGravity = -.05f;
    private float _gravity = -9.8f;
    private readonly float _rotationFactorPerFrame = 15.0f;
    private readonly float _runMultiplier = 3.0f;
    private readonly float _walkMultiplier = 1.5f;

    //Jumping physics configuration
    private float _initialJumpVelocity;
    private readonly float _maxJumpHeight = 1.0f;
    private readonly float _maxJumpTime = 0.5f;

    // Animator hashes
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _jumpTriggerHash;
    
    // State variables
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;

    // Sound FX
    [SerializeField] private AudioClip _runningSound;
    [SerializeField] private AudioClip _walkingSound;
    [SerializeField] private AudioClip _jumpSound;

    // Properties
    public Animator Animator { get { return _animator; } }
    public CharacterController CharacterController { get { return _characterController; } }

    public int JumpTriggerHash { get { return _jumpTriggerHash; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public int IsWalkingHash { get { return _isWalkingHash; } }

    public bool IsMovementPressed { get { return _isMovementPressed; } set { _isMovementPressed = value; } }
    public bool IsRunPressed { get { return _isRunPressed; } set { _isRunPressed = value; } }

    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float CurrentRunMovementY { get { return _currentRunMovement.y; } set { _currentRunMovement.y = value; } }
    public float CurrentMovementX { set { _currentMovement.x = value; } }
    public float CurrentMovementZ { set { _currentMovement.z = value; } }
    public float CurrentMovementInputX { get { return _currentMovementInput.x; } }
    public float CurrentMovementInputY { get { return _currentMovementInput.y; } }
    public float CurrentRunMovementX { set { _currentRunMovement.x = value; } }
    public float CurrentRunMovementZ { set { _currentRunMovement.z = value; } }

    public float GroundedGravity { get { return _groundedGravity; } }
    public float Gravity { get { return _gravity; } }
    public float WalkMultiplier { get { return _walkMultiplier; } }
    public float RunMultiplier { get { return _runMultiplier; } }

    public bool IsJumpPressed { get { return _isJumpPressed; } set { _isJumpPressed = value; } }
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
    public bool IsJumping { set { _isJumping = value; } }
    public float InitialJumpVelocity { get { return _initialJumpVelocity; } }

    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public AudioClip RunningSound { get { return _runningSound; } }
    public AudioClip WalkingSound { get { return _walkingSound; } }
    public AudioClip JumpSound { get { return _jumpSound; } }

    private void Awake()
    {
        InitialiseComponents();
        SetupStateMachine();
        SetupAnimatorHashes();
        SetupInputCallbacks();
        SetupJumpVariables();
    }

    /// <summary>
    /// Called once per frame. Handles character rotation, state updates, and movement.
    /// </summary>
    private void Update()
    {
        HandleRotation();
        _currentState.UpdateStates();
        MoveCharacter();
        Vector3 newPosition = transform.position;
        _agent.Warp(newPosition); // Makes sure NavMeshAgent stays in the correct position when the player leaves a NavMeshSurface
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    } 

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    // Methods called in Awake
    public void InitialiseComponents()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void SetupStateMachine()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void SetupAnimatorHashes()
    {
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _jumpTriggerHash = Animator.StringToHash("jump");
    }

    private void SetupInputCallbacks()
    {
        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;

        _playerInput.CharacterControls.Run.started += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;

        _playerInput.CharacterControls.Jump.started += OnJump;
        _playerInput.CharacterControls.Jump.canceled += OnJump;
    }

    private void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }
    //

    // Input handling
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _requireNewJumpPress = false;
    }
    //

    /// <summary>
    /// Rotates the player based on movement direction.
    /// </summary>
    private void HandleRotation()
    {
        if (!_isMovementPressed) return;

        Vector3 positionToLookAt = new Vector3(
            _isRunPressed ? _currentRunMovement.x : _currentMovement.x,
            0.0f,
            _isRunPressed ? _currentRunMovement.z : _currentMovement.z
        );

        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
    }

    /// <summary>
    /// Moves the character based on the current movement state (walking or running).
    /// </summary>
    private void MoveCharacter()
    {
        Vector3 movement = _isRunPressed ? _currentRunMovement : _currentMovement;
        _characterController.Move(movement * Time.deltaTime);
    }

}
