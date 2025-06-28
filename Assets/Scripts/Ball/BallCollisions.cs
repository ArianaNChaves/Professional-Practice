using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisions : MonoBehaviour
{
    public static event Action OnBallCrash;
    public static event Action<Vector3> OnBallWallCrash;
    
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField, Range(10f, 100f)] private float collisionRange;
    [SerializeField] private PlayerSO playerData;
    private float _damage;
    private const float BallSpeed = 10f;
    
    Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _damage = playerData.Damage;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        //obstacle
        if (Utilities.CompareLayerAndMask(obstacleMask, other.gameObject.layer))
        {
            if (_rigidbody.velocity.magnitude >= BallSpeed)
            {
                OnBallCrash?.Invoke();
                OnBallWallCrash?.Invoke(transform.position);
            }
        }
        
        //Enemy
        if (!Utilities.CompareLayerAndMask(enemyMask, other.gameObject.layer)) return;
        if (!(_rigidbody.velocity.magnitude >= collisionRange)) return;
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            OnBallCrash?.Invoke();
            damageable.TakeDamage(_damage); 
        }
    }
}
