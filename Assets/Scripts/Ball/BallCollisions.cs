using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisions : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    [SerializeField, Range(1f, 11f)] private float collisionRange;
    [SerializeField] private float damage;
    [SerializeField] private Material basicMaterial;
    [SerializeField] private Material canCollisionMaterial;
    
    Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_rigidbody.velocity.magnitude >= collisionRange)
        {
            gameObject.GetComponent<MeshRenderer>().material = canCollisionMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = basicMaterial;
        }
            
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
