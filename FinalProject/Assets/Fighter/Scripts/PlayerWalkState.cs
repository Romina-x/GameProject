using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory){}

    public override void EnterState(){
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
    }
    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.CurrentMovementX = Ctx.CurrentMovementInputX * Ctx.WalkMultiplier;
        Ctx.CurrentMovementZ = Ctx.CurrentMovementInputY * Ctx.WalkMultiplier;
    }
    public override void ExitState(){
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);        
    }
    public override void CheckSwitchStates(){
        // Only switch to run if running is pressed
        if (Ctx.IsRunPressed && Ctx.IsMovementPressed){
            SwitchState(Factory.Run());
        } else if (!Ctx.IsMovementPressed){
            SwitchState(Factory.Idle());
        }
    }
    public override void InitialiseSubState(){}
}
