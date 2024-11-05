using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthAndDamage : MonoBehaviour, IDamageable
{
    private Animator animator;
    private bool isDead = false;
    private int diedTriggerHash;
    private PlayerInput playerInput;

    [SerializeField]
    private int health = 300;

    private void Awake()
    {
        diedTriggerHash = Animator.StringToHash("died");
        animator = GetComponent<Animator>();

        playerInput = new PlayerInput();
        playerInput.CharacterControls.Enable();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;  // Prevent taking damage after death

        health -= damage;
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
    public Transform GetTransform(){
        return transform;
    }
}
