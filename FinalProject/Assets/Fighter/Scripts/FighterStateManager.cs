using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStateManager : MonoBehaviour
{
    FighterBaseState currentState;
    FighterIdle idleState = new FighterIdle();
    FighterWalking fighterWalking = new FighterWalking();
    FighterRunning fighterRunning = new FighterRunning();
    FighterJumping fighterJumping = new FighterJumping();
    FighterAttacking fighterAttacking = new FighterAttacking();
    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    void SwitchState(FighterBaseState state){
        currentState = state;
        state.EnterState(this);
    }
}
