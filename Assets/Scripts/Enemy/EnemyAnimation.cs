using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");
    [SerializeField] private Animator animator;

    public void AttackingAnimation()
    {
        // animator.CrossFade("attack-melee-left", 0f);
        // animator.SetBool(IsAttacking, true);
        animator.SetTrigger(IsAttacking);
        animator.SetBool(IsMoving, false);
    }
    
    public void WalkingAnimation()
    {
        // animator.CrossFade("walk", 0f);
        animator.SetBool(IsMoving, true);
    }
    public void IdlingAnimation()
    {
        // animator.Play("idle");
        animator.SetBool(IsMoving, false);
        animator.SetBool(IsIdle, true);

    }
    public void DeathAnimation()
    {
        animator.Play("die");
        // animator.SetBool(IsMoving, false);
        // // animator.SetBool(IsDeath, true);
        // animator.SetTrigger(IsDead);

    }
}
