using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamagable
{
    //Stats
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;
    //Movement
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected Transform enemyVisual;
    [SerializeField] protected Transform _target;
    
    // protected Transform _target;
    protected Vector3 _targetDirection;
    protected Rigidbody _rigidbody;
    protected enum State
    {
        Idle,
        Moving,
        Attacking
    }
    
    protected State _currentState;

    private void Start()
    {
        _currentState = State.Idle; 
    }

    protected void ChangeState(State newState)
    {
        _currentState = newState;
        switch (_currentState)
        {
            case State.Idle:
            {
                //nose
                break;
            }
            case State.Moving:
            {
                //nose
                break;
            }
            case State.Attacking:
            {
                //attack coroutine
                break;
            }
        }
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
        _target = target;
    }

    protected abstract IEnumerator Moving();
}
