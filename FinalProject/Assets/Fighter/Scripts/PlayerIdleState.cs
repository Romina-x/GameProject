using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State for idle movement (no input detected).
/// </summary>
public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory){}

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
    }
    public override void UpdateState()
    {
        // Make sure player stays still
        CheckSwitchStates();
        Ctx.CurrentMovementX = 0;
        Ctx.CurrentMovementZ = 0;
        Ctx.CurrentRunMovementX = 0;
        Ctx.CurrentRunMovementZ = 0;
    }
    public override void ExitState(){}
    public override void CheckSwitchStates()
    {
        if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
        } 
        else if (Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void InitialiseSubState(){}
}
