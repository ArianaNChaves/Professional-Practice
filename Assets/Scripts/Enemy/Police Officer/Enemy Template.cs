using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    [SerializeField] protected float startingWaitTime;
    [SerializeField] protected GameObject target;
    [SerializeField] protected Animator enemyAnimation;
    [SerializeField] protected EnemySO enemyData;
    [SerializeField] protected LayerMask playerLayer;
    
    protected float MoveSpeed;
    protected float Health;
    protected float Damage;
    protected float AttackRange;
    protected float AttackRate;
    protected float RotationSpeed;
    protected Rigidbody EnemyRigidbody;
    protected Vector3 TargetDirection;
    protected Collider EnemyCollider;
    protected float Distance;
    protected bool IsInRange;
    //Physics Settings
    protected const float BaseDrag = 1;
    protected const float QuietDrag = 100;
    protected const float DeathMass = 1;
    protected const float NormalMass = 100;
    
    protected Dictionary<EnemyState.STATE, EnemyState> States;
    protected EnemyState CurrentState;
    
    protected virtual void Awake()
    {
        MoveSpeed = enemyData.Speed;
        AttackRange = enemyData.AttackRange;
        AttackRate = enemyData.AttackRate;
        Damage = enemyData.Damage;
        Health = enemyData.MaxHealth;
        RotationSpeed = enemyData.RotationSpeed;
    }
    
    
    public IEnumerable<EnemyState.STATE> GetAvailableStates() => States.Keys;
}
