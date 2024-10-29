using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory){
        IsRootState = true;
        InitialiseSubState();
    }
    public override void EnterState(){
        Ctx.CurrentMovementY = Ctx.GroundedGravity;
        Ctx.CurrentRunMovementY = Ctx.GroundedGravity;
    }
    public override void UpdateState(){
        CheckSwitchStates();
    }
    public override void ExitState(){}
    public override void CheckSwitchStates(){
        if(Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress){
            SwitchState(Factory.Jump());
        }
    }
    public override void InitialiseSubState(){
        if(!Ctx.IsMovementPressed && !Ctx.IsRunPressed){
            SetSubState(Factory.Idle());
        } else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed){
            SetSubState(Factory.Walk());
        } else {
            SetSubState(Factory.Run());
        }
    }
}
