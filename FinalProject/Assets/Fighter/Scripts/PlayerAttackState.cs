public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {
        IsRootState = true;
        InitialiseSubState();
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, true);
    }

    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void ExitState(){
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, false);
    }

    public override void InitialiseSubState() {
        if(!Ctx.IsMovementPressed && !Ctx.IsRunPressed){
            SetSubState(Factory.Idle());
        } else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed){
            SetSubState(Factory.Walk());
        } else {
            SetSubState(Factory.Run());
        }
    }
    public override void CheckSwitchStates() {
        if (!Ctx.IsAttackPressed){
            SwitchState(Factory.Grounded());
        }
    }
}
