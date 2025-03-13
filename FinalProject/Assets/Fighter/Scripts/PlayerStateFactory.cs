/// <summary>
/// Factory to create player states
/// </summary>
public class PlayerStateFactory
{
    // Reference to the current context
    private PlayerStateMachine _context;

    // Constructor
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_context, this);
    }
    public PlayerBaseState Run()
    {
        return new PlayerRunState(_context, this);
    }
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }
    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }
}
