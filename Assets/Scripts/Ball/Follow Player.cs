using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float ropeTension   = 20f;
    // [SerializeField] private float initialSpeed  = 10f;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        // Vector3 direction = (transform.position - player.position).normalized;
        // Vector3 tangente = Vector3.Cross(direction, Vector3.up).normalized;
        // // _rigidbody.velocity  = tangente * initialSpeed;
    }
    
    void FixedUpdate()
    {
        JointPhysic();
    }

    private void JointPhysic()
    {
        Vector3 toBall = transform.position - player.position;
        float dist = toBall.magnitude;

        if (dist > maxDistance)
        {
            Vector3 newPosition = player.position + toBall.normalized * maxDistance;
            Vector3 force = (newPosition - transform.position) * ropeTension; 
            _rigidbody.AddForce(force, ForceMode.Acceleration);
        } 
    }
    
}
