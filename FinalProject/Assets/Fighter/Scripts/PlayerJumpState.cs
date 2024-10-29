using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory){
        IsRootState = true;
        InitialiseSubState();
    }

    public override void EnterState(){
        HandleJump();
    }
    public override void UpdateState(){
        CheckSwitchStates();
        HandleGravity();
    }
    public override void ExitState(){
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
        if (Ctx.IsJumpPressed){
            Ctx.RequireNewJumpPress = true;
        }       
    }
    public override void CheckSwitchStates(){
        if (Ctx.CharacterController.isGrounded){
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitialiseSubState(){}

    void HandleJump(){
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
        Ctx.IsJumping = true;
        Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
        Ctx.CurrentRunMovementY = Ctx.InitialJumpVelocity;
    }

    void HandleGravity(){
        if (!Ctx.CharacterController.isGrounded)
        Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
        Ctx.CurrentRunMovementY += Ctx.Gravity * Time.deltaTime;
    }
}
