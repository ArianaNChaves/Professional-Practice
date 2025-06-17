using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseEnemy
{
    public static event Action OnEnemyDeath;

    protected override void Awake()
    {
        base.Awake();
        EnemyRigidbody = GetComponent<Rigidbody>();
        ChangeState(State.Idle);
    }

    private void Update()
    {
        CheckDistance();
    }

    private void FixedUpdate()
    {
        FaceTarget();
    }
    
    protected override void ChangeState(State newState)
    {
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
        StopAllCoroutines();
        EnemyRigidbody.mass = BaseMass;
        enemyAnimation.DeathAnimation();
        EnemyRigidbody.useGravity = false;
        EnemyRigidbody.AddForce(Vector3.up * 8, ForceMode.Impulse);
        OnEnemyDeath?.Invoke();
        Invoke(nameof(DisableAfterSeconds), 5f);
    }

    private void DisableAfterSeconds()
    {
        gameObject.SetActive(false);
    }
    
    protected override IEnumerator Moving()
    {

        EnemyRigidbody.drag = BaseDrag;
        
        
        Vector3 initialVelocity = Vector3.zero;
        if (!target) yield return null;
        while (CurrentState == State.Moving)
        {
            enemyAnimation.WalkingAnimation();
            if (Distance <= AttackRange)
            {
                IsInRange = true;
                ChangeState(State.Attacking);
            }
            TargetDirection = (target.transform.position - EnemyRigidbody.position).normalized;
            Vector3 newVelocity = TargetDirection * MoveSpeed;
            EnemyRigidbody.velocity = Vector3.SmoothDamp(EnemyRigidbody.velocity, newVelocity, ref initialVelocity, 0.1f);
            
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
        EnemyRigidbody.drag = QuietDrag;
        
        // IDamageable targetDamageable = target.GetComponent<IDamageable>();
        // if (targetDamageable == null || !target)
        // {
        //     ChangeState(State.Idle);
        //     yield return null;
        // }
        while (CurrentState == State.Attacking && IsInRange)
        {
            if (Distance > AttackRange)
            {
                IsInRange = false;
                ChangeState(State.Moving);
            }
            EnemyRigidbody.velocity = Vector3.zero;
            
            
            
            
            
            
            enemyAnimation.AttackingAnimation();
            // targetDamageable?.TakeDamage(Damage);
            yield return new WaitForSeconds(AttackRate);
            yield return new WaitForFixedUpdate();
        }
    }
    
    private void FaceTarget()
    {
        if (!target) return;
        Vector3 newDirection = (target.transform.position - transform.position).normalized;
        newDirection.y = 0f;
        if (newDirection.sqrMagnitude < 0.001f) return;
        Quaternion newRotation = Quaternion.LookRotation(newDirection);
        EnemyRigidbody.MoveRotation(Quaternion.Slerp(EnemyRigidbody.rotation, newRotation, rotationSpeed * Time.fixedDeltaTime));
    }

    private void CheckDistance()
    {
        Distance = Vector3.Distance(transform.position, target.transform.position);
    }
}
