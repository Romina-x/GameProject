using UnityEngine;

public abstract class FighterBaseState : MonoBehaviour
{
    public abstract void EnterState(FighterStateManager fighter);

    public abstract void UpdateState(FighterStateManager fighter);

    public abstract void OnCollisionEnter(FighterStateManager fighter);
}
