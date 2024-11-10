using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthAndDamage : MonoBehaviour, IDamageable
{
    private Animator animator;
    private bool isDead = false;
    private int diedTriggerHash;
    private int attackTriggerHash;
    private int gotHitTriggerHash;
    private PlayerInput playerInput;
    private bool attacked = false;

    [SerializeField]
    private int health = 300;

    void Awake()
    {
        animator = GetComponent<Animator>();     
        playerInput = new PlayerInput();

        attackTriggerHash = Animator.StringToHash("attack");
        gotHitTriggerHash = Animator.StringToHash("gotHit");
        diedTriggerHash = Animator.StringToHash("died");
        playerInput.CharacterControls.Attack.performed += OnAttack;

    }
    void OnAttack(InputAction.CallbackContext context){
        Debug.Log("Attack button pressed");
        animator.SetTrigger(attackTriggerHash);
    }


    public void TakeDamage(int damage)
    {
        if (isDead) return;  // Prevent taking damage after death
        health -= damage;

        if (health > 0) {
            animator.SetTrigger(gotHitTriggerHash);
        }

        if (health <= 0)
        {
            isDead = true;
            animator.SetTrigger(diedTriggerHash);
        }
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    public Transform GetTransform(){
        return transform;
    }
    public string GetName(){
        return "Player";
    }
}
