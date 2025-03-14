using UnityEngine;
/// <summary>
/// Base class for all player states in the state machine.
/// </summary>
public abstract class PlayerBaseState
{
    // Current context (state machine)
    private PlayerStateMachine _ctx;

    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSubState;
    private PlayerBaseState _currentSuperState;
    private bool _isRootState = false;

    // Properties
    protected bool IsRootState { set { _isRootState = value; } }
    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }
    protected PlayerBaseState CurrentSubState { get { return _currentSubState; } }

    // Constructor
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    // Abstract methods
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitialiseSubState();

    // Calls UpdateState() on this state and the substate if it is active
    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }

    // Exits the current state and enters a new one, updating the state machine context
    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();
        if (_isRootState)
        {
            _ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
        Debug.Log(newState);
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
