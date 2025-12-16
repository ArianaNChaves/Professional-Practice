using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeprecatedEnemy : BaseEnemy
{
    public static event Action OnEnemyDeath;
    public static event Action<DeprecatedEnemy> OnSpawn;
    
    [SerializeField] private float deathForce;
    [SerializeField] private float timeToDespawn;
    [SerializeField] private bool isInScene = false;
    [SerializeField] private GameObject player;
    
    private Collider _collider;
    private IDamageable _targetDamageable;
    private bool _isPlayer = false;
    private List<GameObject> _targetsList;
    private bool _isDead;

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

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Activate()
    {
        _isDead = false;
        Health = enemyData.MaxHealth;
        // EnemyRigidbody.velocity = Vector3.zero;
        // EnemyRigidbody.useGravity = true;
        isInScene = true;
        _isPlayer = false;
        IsInRange = false;
        // if (_targetsList != null && _targetsList.Count > 0)
        // {
        //     target = _targetsList[0];
        // }
        // else if (player)
        // {
        //     target = player;
        // }
        // _collider.enabled = true;
        // EnemyRigidbody.mass = NormalMass;
        // EnemyRigidbody.drag = QuietDrag;
        OnSpawn?.Invoke(this);
        ChangeState(State.Idle);
    }
    
    protected override void Death()
    {
        if (_isDead) return; // guard against re-entry
        _isDead = true;
        // EnemyRigidbody.useGravity = false;
        isInScene = false;
        // _collider.enabled = false;
        _targetsList.Clear();
        OnEnemyDeath?.Invoke();
        // EnemyRigidbody.drag = BaseDrag;
        // EnemyRigidbody.mass = DeathMass;
        enemyAnimation.DeathAnimation();
        // EnemyRigidbody.AddForce(Vector3.up * deathForce, ForceMode.Impulse);
        // EnemyRigidbody.useGravity = true;
        StopAllCoroutines();
        ReturnObjectToPool(); //todo que espere un tiempo antes de desaparecer para que se vea la animacion de muerte
    }

    private IEnumerator DespawnAfter(float t)
    {
        if (t > 0f)
        {
            yield return new WaitForSeconds(t);
        }
    }
    
    protected override void ChangeState(State newState)
    {
        if (_isDead && newState != State.Death)
        {
            return; 
        }
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
                if (!target || !Utilities.CompareLayerAndMask(playerLayer, target.layer))
                {
                    StateRoutine = StartCoroutine(Idling());
                    break;
                }
                _targetDamageable = target.GetComponent<IDamageable>();
                StateRoutine = StartCoroutine(Attacking());
                break;
            }
            case State.Death:
            {
                Death();
                break;
            }
            default:
                throw new NotImplementedException("Enemy State no implementado");
        }
    }
    
    public override void ReturnObjectToPool()
    {
        // this.gameObject.SetActive(false);
        PoolManager.Instance.ReturnToPool(this);
    }

    protected override IEnumerator Moving()
    {
        Vector3 initialVelocity = Vector3.zero;
        if (!target)
        {
            ChangeState(State.Idle);
            yield break;
        }
        while (CurrentState == State.Moving)
        {
            FaceTarget();
            enemyAnimation.WalkingAnimation();
            if (ReachTarget() && _isPlayer)
            {
                IsInRange = true;
                ChangeState(State.Attacking);
                yield break;
            }
            if (ReachTarget() && !_isPlayer)
            {
                ChangeState(State.Idle);
                yield break;
            }
            if (!target)
            {
                ChangeState(State.Idle);
                yield break;
                continue;
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
        if (!target && player)
        {
            target = player;
        }
        if (target)
        {
            FaceTarget();
            ChangeState(State.Moving);
            yield break;
        }
    }
    
    protected override IEnumerator Attacking()
    {
        if (_targetDamageable == null || !player)
        {
            ChangeState(State.Idle);
            yield break;
        }
        while (CurrentState == State.Attacking && IsInRange)
        {
            Distance = Vector3.Distance(transform.position, player.transform.position);
            EnemyRigidbody.velocity = Vector3.zero;
            FaceTarget();
            if (Distance > AttackRange)
            {
                IsInRange = false;
                ChangeState(State.Moving);
                yield break;
            }
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
    
    public void SetTargetPlayer(GameObject newTarget)
    {
        _targetsList.Add(newTarget);
        player = newTarget;
    }
    private void CheckTargets()
    {
        if (_targetsList.Count > 0)
        {
            target = _targetsList[0];
            
            if (!Utilities.CompareLayerAndMask(playerLayer, target.layer))
            {
                _targetsList.RemoveAt(0);
            }
            else
            {
                player = target;
            }
        }
    }

    // public void SetPlayer(GameObject newPlayer)
    // {
    //     player = newPlayer;
    // }
    public override void TakeDamage(float damage)
    {
        if (_isDead) return;
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            if (CurrentState != State.Death)
            {
                ChangeState(State.Death);
            }
        }
    }

    private bool ReachTarget()
    {
        if (!target) return false;
        _isPlayer = Utilities.CompareLayerAndMask(playerLayer, target.layer);
        return Vector3.Distance(transform.position, target.transform.position) < AttackRange;
    }

}
