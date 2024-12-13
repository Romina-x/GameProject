using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State for player run input
public class PlayerRunState : PlayerBaseState
{
    // Constructor
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory){}

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        Ctx.CurrentRunMovementX = Ctx.CurrentMovementInputX * Ctx.RunMultiplier; //run speed multiplier
        Ctx.CurrentRunMovementZ = Ctx.CurrentMovementInputY * Ctx.RunMultiplier;
    }

    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsRunPressed && Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        } 
        else if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
    }
    
    public override void InitialiseSubState(){}
}
