using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisions : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    [SerializeField, Range(1f, 11f)] private float collisionRange;
    [SerializeField] private float damage;
    
    Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {

        if (Utilities.CompareLayerAndMask(collisionMask, other.gameObject.layer))
        {
            if (_rigidbody.velocity.magnitude >= collisionRange)
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage); 
                }
                
            }
        }
    }
}
