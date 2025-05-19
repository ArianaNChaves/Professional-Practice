using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseEnemy
{
    private void Awake()
    {
        EnemyRigidbody = GetComponent<Rigidbody>();
        ChangeState(State.Idle);
    }

    private void Update()
    {
        CheckDistance();
    }
    

    protected override void ChangeState(State newState)
    {
        Debug.Log($"State: {newState}");
        StopAllCoroutines();
        CurrentState = newState;
        switch (CurrentState)
        {
            case State.Idle:
            {
                StartCoroutine(Idling());
                break;
            }
            case State.Moving:
            {
                StartCoroutine(Moving());
                break;
            }
            case State.Attacking:
            {
                StartCoroutine(Attacking());
                break;
            }
            default:
                Debug.LogError($"Enemy.cs - ChangeState: State {newState} no existe");
                break;
        }
    }

    protected override void Death()
    {
        enemyAnimation.DeathAnimation();
    }
    
    protected override IEnumerator Moving()
    {
        Vector3 initialVelocity = Vector3.zero;
        if (!target) yield return null;
        while (CurrentState == State.Moving)
        {
            enemyAnimation.WalkingAnimation();
            if (Distance <= attackRange)
            {
                IsInRange = true;
                ChangeState(State.Attacking);
            }
            TargetDirection = (target.transform.position - EnemyRigidbody.position).normalized;
            Vector3 newVelocity = TargetDirection * moveSpeed;
            EnemyRigidbody.velocity = Vector3.SmoothDamp(EnemyRigidbody.velocity, newVelocity, ref initialVelocity, 0.1f);

            if (EnemyRigidbody.velocity.sqrMagnitude > 0.01f)
            {
                Quaternion newRotation = Quaternion.LookRotation(target.transform.position);
                EnemyRigidbody.MoveRotation(Quaternion.Slerp(EnemyRigidbody.rotation, newRotation, rotationSpeed * Time.fixedDeltaTime));
            }
            
            yield return new WaitForFixedUpdate();
        }
    }
    
    protected override IEnumerator Idling()
    {
        enemyAnimation.IdlingAnimation();
        yield return new WaitForSeconds(startingWaitTime);
        ChangeState(State.Moving);
    }

    protected override IEnumerator Attacking()
    {
        IDamageable targetDamageable = target.GetComponent<IDamageable>();
        if (targetDamageable == null || !target)
        {
            ChangeState(State.Idle);
            yield return null;
        }
        while (CurrentState == State.Attacking && IsInRange)
        {
            EnemyRigidbody.velocity = Vector3.zero;
            if (Distance > attackRange)
            {
                IsInRange = false;
                ChangeState(State.Moving);
            }

            enemyAnimation.AttackingAnimation();
            targetDamageable.TakeDamage(damage);
            yield return new WaitForSeconds(attackRate);
            
            yield return new WaitForFixedUpdate();
        }
    }

    private void CheckDistance()
    {
        Distance = Vector3.Distance(transform.position, target.transform.position);
    }
}
