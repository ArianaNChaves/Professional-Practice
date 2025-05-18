using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseEnemy
{
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        ChangeState(State.Idle);
    }

    private void Update()
    {
        CheckDistance();
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
    
    protected override IEnumerator Moving()
    {
        Vector3 initialVelocity = Vector3.zero;
        while (CurrentState == State.Moving)
        {
            if (Distance <= attackRange)
            {
                IsInRange = true;
                ChangeState(State.Attacking);
            }
            TargetDirection = (target.position - Rigidbody.position).normalized;
            Vector3 newVelocity = TargetDirection * moveSpeed;
            Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, newVelocity, ref initialVelocity, 0.1f);

            if (Rigidbody.velocity.sqrMagnitude > 0.01f)
            {
                Quaternion newRotation = Quaternion.LookRotation(Rigidbody.velocity);
                Rigidbody.MoveRotation(Quaternion.Slerp(Rigidbody.rotation, newRotation, rotationSpeed * Time.fixedDeltaTime));
            }
            yield return new WaitForFixedUpdate();
        }
    }

    protected override IEnumerator Idling()
    {
        yield return new WaitForSeconds(startingWaitTime);
        ChangeState(State.Moving);
    }

    protected override IEnumerator Attacking()
    {
        while (CurrentState == State.Attacking && IsInRange)
        {
            Rigidbody.velocity = Vector3.zero;
            if (Distance > attackRange)
            {
                IsInRange = false;
                ChangeState(State.Moving);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void CheckDistance()
    {
        Distance = Vector3.Distance(transform.position, target.position);
    }
}
