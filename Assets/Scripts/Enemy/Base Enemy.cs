using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseEnemy : MonoBehaviour, IDamagable
{
    //Stats
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;
    //Movement
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float startingWaitTime;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected Transform enemyVisual;
    [SerializeField] protected Transform target;
    
    // protected Transform _target;
    protected Vector3 TargetDirection;
    protected Rigidbody Rigidbody;
    protected float Distance;
    protected bool IsInRange;
    protected enum State
    {
        Idle,
        Moving,
        Attacking
    }
    
    protected State CurrentState;
    
    protected virtual void ChangeState(State newState)
    {
        CurrentState = newState;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }

    protected void Death()
    {
        gameObject.SetActive(false);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    protected abstract IEnumerator Moving();
    protected abstract IEnumerator Idling();
    protected abstract IEnumerator Attacking();
}
