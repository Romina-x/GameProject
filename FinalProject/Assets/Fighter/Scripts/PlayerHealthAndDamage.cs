using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthAndDamage : MonoBehaviour, IDamageable
{
    [SerializeField]
    private AttackRadius attackRadius;
    private Animator animator;

    [SerializeField]
    private int health = 300;
    private int attackTriggerHash;
    private PlayerInput playerInput;

    private void Awake(){
        attackTriggerHash = Animator.StringToHash("attack");
        animator = GetComponent<Animator>();

        playerInput = new PlayerInput();
        playerInput.CharacterControls.Attack.started += OnAttack;
    }
    private void OnAttack(InputAction.CallbackContext context)
    {
        // Play the attack animation regardless of whether there's an enemy in range
        animator.SetTrigger(attackTriggerHash);

        // Check if an enemy is within the attack radius
        IDamageable target = attackRadius.GetClosestTarget();
        if (target != null)
        {   
            // If an enemy is within range, deal damage
            target.TakeDamage(attackRadius.damage);
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0) {
            gameObject.SetActive(false);
        }
    }

    public Transform GetTransform(){
        return transform;
    }
    void OnEnable() 
    {
        playerInput.CharacterControls.Enable();
    }
    void OnDisable() 
    {
        playerInput.CharacterControls.Disable();
    }
}
