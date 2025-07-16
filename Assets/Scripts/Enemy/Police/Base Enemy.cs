using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseEnemy : MonoBehaviour, IDamageable, IPooleable
{
    [SerializeField] protected float startingWaitTime;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected GameObject target;
    [SerializeField] protected EnemyAnimation enemyAnimation;
    [SerializeField] protected EnemySO enemyData;
    [SerializeField] protected LayerMask playerLayer;
    
    protected float MoveSpeed;
    protected float Health;
    protected float Damage;
    protected float AttackRange;
    protected float AttackRate;
    protected Rigidbody EnemyRigidbody;
    protected Vector3 TargetDirection;
    protected Coroutine StateRoutine;
    protected float Distance;
    protected bool IsInRange;
    //Physics Settings
    protected const float BaseDrag = 1;
    protected const float QuietDrag = 100;
    protected const float DeathMass = 1;
    protected const float NormalMass = 100;
    protected State CurrentState;
    public enum State
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

    public virtual void ChangeTarget()
    {
        if (target) return;
        CurrentState = State.Idle;
    }
    
    protected virtual void ChangeState(State newState)
    {
        CurrentState = newState;
    }

    public abstract void TakeDamage(float damage);


    protected abstract IEnumerator Death();
    protected abstract IEnumerator Moving();
    protected abstract IEnumerator Idling();
    protected abstract IEnumerator Attacking();

    public abstract void ReturnObjectToPool();

}
