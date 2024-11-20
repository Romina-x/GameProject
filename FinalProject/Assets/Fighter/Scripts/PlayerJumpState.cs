using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root state for player jump movement
public class PlayerJumpState : PlayerBaseState
{
    // Constructor
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitialiseSubState();
    }

    public override void EnterState()
    {
        HandleJump();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        HandleGravity();
    }

    public override void ExitState()
    {
        if (Ctx.IsJumpPressed)
        {
            Ctx.RequireNewJumpPress = true;
        }       
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitialiseSubState(){}

    private void HandleJump(){
        Ctx.Animator.SetTrigger(Ctx.JumpTriggerHash);
        Ctx.IsJumping = true;
        Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
        Ctx.CurrentRunMovementY = Ctx.InitialJumpVelocity;
    }

    private void HandleGravity(){
        if (!Ctx.CharacterController.isGrounded)
        {
            Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
            Ctx.CurrentRunMovementY += Ctx.Gravity * Time.deltaTime;
        } 
    }
}
