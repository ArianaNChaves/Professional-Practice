using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void AttackingAnimation()
    {
        animator.CrossFade("attack-melee-left", 0f);
    }
    public void WalkingAnimation()
    {
        animator.CrossFade("walk", 0f);
    }
    public void IdlingAnimation()
    {
        animator.Play("idle");
    }
    public void DeathAnimation()
    {
        animator.Play("die");
    }
}
