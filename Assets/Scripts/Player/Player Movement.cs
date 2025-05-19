using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    //todo Cambiarlo a un scriptable object con los settings del movimiento del player
    [SerializeField] private Transform playerVisual;
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private float rotationSpeed = 10f;
    
    [SerializeField] private Animator animator;
    private float _speed;
    private Vector2 _input;
    private Rigidbody _rigidbody;
    private bool _canMove = true;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speed = playerData.Speed;
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
        if (!_canMove)
        {
            return;
        }
        if (_input.sqrMagnitude < Mathf.Epsilon)
        {
            return;
        }
        
        Vector3 direction = transform.right * _input.x + transform.forward * _input.y;

        direction.Normalize();
        
        Rotation(direction);
        
        Vector3 newPosition = _rigidbody.position + direction * (_speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
        
    }

    private void Rotation(Vector3 direction)
    {
        Quaternion newRotation = Quaternion.LookRotation(direction);
        Quaternion smoothRotation = Quaternion.Slerp(playerVisual.rotation, newRotation,rotationSpeed * Time.fixedDeltaTime);
        playerVisual.rotation = smoothRotation;
    }

    public void CanMove(bool canMove)
    {
        _canMove = canMove;
    }

    
}
