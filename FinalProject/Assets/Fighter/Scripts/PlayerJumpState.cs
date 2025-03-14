using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Root state for player jump movement. Handles the player's jump logic and gravity application.
/// </summary>
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
        // Stop running or walking sounds while in the air and play jump sound
        SoundFXManager.instance.StopLoopingSoundFX("Running");
        SoundFXManager.instance.StopLoopingSoundFX("Walking");
        SoundFXManager.instance.PlaySoundFX(Ctx.JumpSound, Ctx.transform, 1f);
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

    /// <summary>
    /// Handles the jump logic, applying the initial jump velocity to the player.
    /// </summary>
    private void HandleJump()
    {
        //Ctx.Animator.SetTrigger(Ctx.JumpTriggerHash);
        Ctx.Animator.ResetTrigger(Ctx.JumpTriggerHash); // Reset first
        Ctx.Animator.SetTrigger(Ctx.JumpTriggerHash);
        Ctx.IsJumping = true;
        Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
        Ctx.CurrentRunMovementY = Ctx.InitialJumpVelocity;
    }

    /// <summary>
    /// Applies gravity to the player every frame to fall back down.
    /// </summary>
    private void HandleGravity()
    {
        if (!Ctx.CharacterController.isGrounded)
        {
            Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
            Ctx.CurrentRunMovementY += Ctx.Gravity * Time.deltaTime;
        }
    }
}
