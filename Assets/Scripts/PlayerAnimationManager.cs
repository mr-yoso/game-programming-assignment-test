using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    public void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
    }
    public void LateUpdate()
    {
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        animator.SetFloat("CharacterSpeed", movement.GetAnimationSpeed());
        animator.SetBool("IsFalling", !movement.isGrounded);

        if (movement.isGrounded)
        {
            animator.SetBool("IsFalling", false);
        }

        if (movement.jumpCount == 2)
        {
            animator.SetTrigger("doFlip");
            movement.jumpCount++;
        }
        
    }
}
