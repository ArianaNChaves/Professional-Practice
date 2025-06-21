using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisions : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    [SerializeField, Range(10f, 100f)] private float collisionRange;
    [SerializeField] private PlayerSO playerData;
    private float _damage;
    
    Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _damage = playerData.Damage;
    }

    private void Update()
    {
        Debug.Log($"Ball velocity: {_rigidbody.velocity.magnitude}");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!Utilities.CompareLayerAndMask(collisionMask, other.gameObject.layer)) return;
        if (!(_rigidbody.velocity.magnitude >= collisionRange)) return;
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage); 
        }
    }
}
