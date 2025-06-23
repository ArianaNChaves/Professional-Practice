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
            Activate();
        }
    }
    

    public void Activate()
    {
        EnemyRigidbody.useGravity = true;
        _collider.enabled = true;
        EnemyRigidbody.mass = NormalMass;
        OnSpawn?.Invoke(this);
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
                _targetDamageable = target.GetComponent<IDamageable>();
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
        EnemyRigidbody.mass = DeathMass;
        enemyAnimation.DeathAnimation();
        EnemyRigidbody.AddForce(Vector3.up * deathForce, ForceMode.Impulse);
        OnEnemyDeath?.Invoke();
        yield return new WaitForSeconds(timeToDespawn);
        ReturnObjectToPool();
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
            Distance = Vector3.Distance(transform.position, target.transform.position);
            FaceTarget();
            enemyAnimation.WalkingAnimation();
            if (Distance <= AttackRange && _isPlayer)
            {
                IsInRange = true;
                ChangeState(State.Attacking);
            }
            if (Distance <= 0.1f && !_isPlayer)
            {
                CheckTargets();
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
            ChangeState(State.Moving);  
        }
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
            Distance = Vector3.Distance(transform.position, target.transform.position);
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
    
    public void SetTargetList(GameObject[] newTarget)
    {
        _targetsList.Clear();
        _targetsList.AddRange(newTarget);
    }
    private void CheckTargets()
    {
        Debug.Log("ENEMYT Targets: " + _targetsList.Count);

        if (_targetsList.Count > 0)
        {
            target = _targetsList[0];
            _targetsList.RemoveAt(0);
            _isPlayer = Utilities.CompareLayerAndMask(playerLayer, target.layer);
        }
    }

}
