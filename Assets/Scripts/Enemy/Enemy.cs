using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseEnemy
{
    [SerializeField] private float deathForce;
    [SerializeField] private float timeToDespawn;
    public static event Action OnEnemyDeath;
    private Collider _collider;
    private IDamageable _targetDamageable;

    protected override void Awake()
    {
        base.Awake();
        EnemyRigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        ChangeState(State.Idle);
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
            case State.Death:
            {
                StartCoroutine(Death());
                break;
            }
            default:
                throw new NotImplementedException("Enemy State no implementado");
        }
    }

    protected override IEnumerator Death()
    {
        EnemyRigidbody.useGravity = false;
        _collider.enabled = false;
        EnemyRigidbody.drag = BaseDrag;
        EnemyRigidbody.mass = BaseMass;
        enemyAnimation.DeathAnimation();
        EnemyRigidbody.AddForce(Vector3.up * deathForce, ForceMode.Impulse);
        OnEnemyDeath?.Invoke();
        yield return new WaitForSeconds(timeToDespawn);
        gameObject.SetActive(false);

    }
    
    protected override IEnumerator Moving()
    {
        EnemyRigidbody.drag = BaseDrag;
        Vector3 initialVelocity = Vector3.zero;
        if (!target)
        {
            ChangeState(State.Idle);
            yield return null;
        }
        while (CurrentState == State.Moving)
        {
            CheckDistance();
            FaceTarget();
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
        EnemyRigidbody.drag = QuietDrag;
        Vector3 initialVelocity = Vector3.zero;
        enemyAnimation.IdlingAnimation();
        if (target)
        {
            _targetDamageable = target.GetComponent<IDamageable>();
            FaceTarget();
            ChangeState(State.Moving);  
        }
        yield return new WaitForSeconds(startingWaitTime);
    }
    
    protected override IEnumerator Attacking()
    {
        EnemyRigidbody.drag = QuietDrag;
        if (_targetDamageable == null || !target)
        {
            ChangeState(State.Idle);
            yield return null;
        }
        while (CurrentState == State.Attacking && IsInRange)
        {
            CheckDistance();
            FaceTarget();
            if (Distance > AttackRange)
            {
                IsInRange = false;
                ChangeState(State.Moving);
            }
            EnemyRigidbody.velocity = Vector3.zero;
            enemyAnimation.AttackingAnimation();
            _targetDamageable?.TakeDamage(Damage);
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
