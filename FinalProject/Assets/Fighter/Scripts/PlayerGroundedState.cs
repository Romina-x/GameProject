using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Root state for the player when grounded. It also applies gravity when grounded or in the air.
/// </summary>
public class PlayerGroundedState : PlayerBaseState
{
    // Constructor
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitialiseSubState();
    }

    /// <summary>
    /// Called when the state is entered. Sets up sound effects based on the sub-state (running or walking).
    /// </summary>
    public override void EnterState()
    {
        this.CurrentSubState.EnterState();

        PlayerBaseState subState = this.CurrentSubState;

        if (subState is PlayerRunState)
        {
            SoundFXManager.instance.StartLoopingSoundFX("Running", Ctx.RunningSound, Ctx.transform, 1f);
        }
        else if (subState is PlayerWalkState)
        {
            SoundFXManager.instance.StartLoopingSoundFX("Walking", Ctx.WalkingSound, Ctx.transform, 1f);
        }
        else
        {
            
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        ApplyGroundedGravity();
    }

    public override void ExitState(){}

    public override void CheckSwitchStates()
    {
        if(Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchState(Factory.Jump());
        }
    }

    /// <summary>
    /// Initializes the sub-state based on the player's current input (idle, walking, or running).
    /// </summary
    public override void InitialiseSubState()
    {
        if(!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Idle());
        } 
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Walk());
        } 
        else 
        {
            SetSubState(Factory.Run());
        }
    }

    /// <summary>
    /// Applies gravity to the player based on whether they are grounded or in the air.
    /// </summary>
    void ApplyGroundedGravity() 
    {
        if (Ctx.CharacterController.isGrounded) 
        {
            if (Ctx.CurrentMovementY < 0) 
            {
                Ctx.CurrentMovementY = Ctx.GroundedGravity; // Reset to grounded gravity when on ground
                Ctx.CurrentRunMovementY = Ctx.GroundedGravity;
            }
        } 
        else 
        {
            // Apply regular gravity if not grounded
            Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
            Ctx.CurrentRunMovementY += Ctx.Gravity * Time.deltaTime;
        }
    }
}
