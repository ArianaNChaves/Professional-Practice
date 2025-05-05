using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    //todo Cambiarlo a un scriptable object con los settings del movimiento del player
    [SerializeField] private float speed;
    
    private Vector2 _input;
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        Move();
        
    }
    
    public void OnMovement(Vector2 context)
    {
        _input = context;
        
    }

    private void Move()
    {
        if (_input.sqrMagnitude < Mathf.Epsilon) return;
        
        Vector3 direction = transform.right * _input.x + transform.forward * _input.y;

        direction.Normalize();

        Vector3 newPosition = _rigidbody.position + direction * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
        
    }

    


}
