using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float ropeTension   = 20f;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        JointPhysic();
    }

    private void JointPhysic()
    {
        Vector3 toBall = transform.position - player.position;
        float dist = toBall.magnitude;

        if (!(dist > maxDistance)) return;
        
        Vector3 newPosition = player.position + toBall.normalized * maxDistance;
        Vector3 force = (newPosition - transform.position) * ropeTension; 
        _rigidbody.AddForce(force, ForceMode.Acceleration);
    }
    
}
