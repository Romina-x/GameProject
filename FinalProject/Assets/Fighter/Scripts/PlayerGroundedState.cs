using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root state for player on the ground
public class PlayerGroundedState : PlayerBaseState
{
    // Constructor
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitialiseSubState();
    }

    public override void EnterState()
    {
        PlayerBaseState subState = this.CurrentSubState;

        if (subState is PlayerRunState)
        {
            SoundFXManager.instance.StartLoopingSoundFX("Running", Ctx.RunningSound, Ctx.transform, 1f);
        }
        else if (subState is PlayerWalkState)
        {
            SoundFXManager.instance.StartLoopingSoundFX("Walking", Ctx.WalkingSound, Ctx.transform, 1f);
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

    // Sets the substate based on the player's input
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
