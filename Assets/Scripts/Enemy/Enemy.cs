using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseEnemy
{
    public static event Action OnEnemyDeath;
    public static event Action<Enemy> OnSpawn;
    [SerializeField] private float deathForce;
    [SerializeField] private float timeToDespawn;
    [SerializeField] private bool isInScene = false;
    [SerializeField] private GameObject player;
    
    private Collider _collider;
    private IDamageable _targetDamageable;
    private bool _isPlayer = false;
    private List<GameObject> _targetsList;

    protected override void Awake()
    {
        base.Awake();
        EnemyRigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _targetsList = new List<GameObject>();
    }

    private void Start()
    {
        if (isInScene)
        {
            ChangeState(State.Idle);
        }
    }
    

    public void Activate()
    {
        Health = enemyData.MaxHealth;
        EnemyRigidbody.velocity = Vector3.zero;
        EnemyRigidbody.useGravity = true;
        _collider.enabled = true;
        EnemyRigidbody.mass = NormalMass;
        EnemyRigidbody.drag = QuietDrag;
        ChangeState(State.Idle);
        OnSpawn?.Invoke(this);
    }
    
    protected override IEnumerator Death()
    {
        EnemyRigidbody.useGravity = false;
        isInScene = false;
        _collider.enabled = false;
        _targetsList.Clear();
        EnemyRigidbody.drag = BaseDrag;
        EnemyRigidbody.mass = DeathMass;
        enemyAnimation.DeathAnimation();
        EnemyRigidbody.AddForce(Vector3.up * deathForce, ForceMode.Impulse);
        OnEnemyDeath?.Invoke();
        yield return new WaitForSeconds(timeToDespawn);
        ReturnObjectToPool();
    }
    
    protected override void ChangeState(State newState)
    {
        if (StateRoutine != null)
        {
            StopCoroutine(StateRoutine); 
        }
                
        CurrentState = newState;
        switch (CurrentState)
        {
            case State.Idle:
            {
                EnemyRigidbody.drag = QuietDrag;
                StateRoutine = StartCoroutine(Idling());
                break;
            }
            case State.Moving:
            {
                EnemyRigidbody.drag = BaseDrag;
                StateRoutine = StartCoroutine(Moving());
                break;
            }
            case State.Attacking:
            {
                EnemyRigidbody.drag = QuietDrag;
                _targetDamageable = target.GetComponent<IDamageable>();
                StateRoutine = StartCoroutine(Attacking());
                break;
            }
            case State.Death:
            {
                StateRoutine = StartCoroutine(Death());
                break;
            }
            default:
                throw new NotImplementedException("Enemy State no implementado");
        }
    }
    
    public override void ReturnObjectToPool()
    {
        StopAllCoroutines();
        PoolManager.Instance.ReturnToPool(this);
    }

    protected override IEnumerator Moving()
    {
        Vector3 initialVelocity = Vector3.zero;
        if (!target)
        {
            yield return null;
            ChangeState(State.Idle);
        }
        while (CurrentState == State.Moving)
        {
            FaceTarget();
            enemyAnimation.WalkingAnimation();
            if (ReachTarget() && _isPlayer)
            {
                IsInRange = true;
                yield return null;
                ChangeState(State.Attacking);
            }
            if (ReachTarget() && !_isPlayer)
            {
                yield return null;
                ChangeState(State.Idle);
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
        CheckTargets();
        if (isInScene)
        {
            target = player;
        }
        if (target)
        {
            FaceTarget();
            yield return null;
            ChangeState(State.Moving);
        }
    }
    
    protected override IEnumerator Attacking()
    {
        if (_targetDamageable == null || !player)
        {
            yield return null;
            ChangeState(State.Idle);
        }
        while (CurrentState == State.Attacking && IsInRange)
        {
            Distance = Vector3.Distance(transform.position, player.transform.position);
            FaceTarget();
            if (Distance > AttackRange)
            {
                IsInRange = false;
                yield return null;
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

    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        _targetsList.Add(newSpawnPoint.gameObject);
    }
    
    public void SetTargetList(GameObject[] newTarget)
    {
        _targetsList.AddRange(newTarget);
    }
    private void CheckTargets()
    {
        if (_targetsList.Count > 0)
        {
            target = _targetsList[0];
            if (!_isPlayer)
            {
                _targetsList.RemoveAt(0);
            }
            else
            {
                player = target;
            }
        }
    }

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            ChangeState(State.Death);
        }
    }

    private bool ReachTarget()
    {
        _isPlayer = Utilities.CompareLayerAndMask(playerLayer, target.layer);
        return Vector3.Distance(transform.position, target.transform.position) < AttackRange;
    }

}
