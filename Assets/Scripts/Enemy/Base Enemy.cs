using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float startingWaitTime;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected GameObject target;
    [SerializeField] protected EnemyAnimation enemyAnimation;
    [SerializeField] protected EnemySO enemyData;
    
    protected float MoveSpeed;
    protected float Health;
    protected float Damage;
    protected float AttackRange;
    protected float AttackRate;
    protected Vector3 TargetDirection;
    protected Rigidbody EnemyRigidbody;
    protected float Distance;
    protected bool IsInRange;
    //Physics Settings
    protected const float BaseDrag = 1;
    protected const float QuietDrag = 100;
    protected const float BaseMass = 1;
    protected enum State
    {
        Idle,
        Moving,
        Attacking,
        Death,
    }

    protected virtual void Awake()
    {
        MoveSpeed = enemyData.Speed;
        AttackRange = enemyData.AttackRange;
        AttackRate = enemyData.AttackRate;
        Damage = enemyData.Damage;
        Health = enemyData.MaxHealth;
    }
    
    protected State CurrentState;
    
    protected virtual void ChangeState(State newState)
    {
        CurrentState = newState;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Taking Damage");
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            ChangeState(State.Death);
        }
    }

    protected abstract IEnumerator Death();

    public void SetTarget(GameObject newTarget)
    {
        this.target = newTarget;
    }

    protected abstract IEnumerator Moving();
    protected abstract IEnumerator Idling();
    protected abstract IEnumerator Attacking();
}
