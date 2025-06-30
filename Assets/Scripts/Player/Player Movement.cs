using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform playerVisual;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private float rotationSpeed = 10f;
    
    [SerializeField] private Animator animator;
    private float _speed;
    private Vector2 _input;
    private Rigidbody _rigidbody;
    private bool _canMove = true;
    private bool _isMoving;
    private Coroutine _moveCoroutine;
    
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
        PlayMoveSound();
    }
    private void PlayMoveSound()
    {
        float moveSound = AudioManager.Instance.GetSFXSound("Player Movement").clip.length;
        if (_moveCoroutine == null) _moveCoroutine = StartCoroutine(MoveSound(moveSound));
    }
    
    private void StopMoveSound()
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }

    private IEnumerator MoveSound(float duration)
    {
        while (_isMoving)
        {
            AudioManager.Instance.PlayEffect("Player Movement");
            yield return new WaitForSeconds(duration);
        }
    }

    private void Move()
    {
        if (!_canMove)
        {
            return;
        }
        if (_input.sqrMagnitude < Mathf.Epsilon)
        {
            StopMoveSound();
            return;
        }

        _isMoving = _rigidbody.velocity.magnitude > 0.1f;
        
        Vector3 direction = transform.right * _input.x + transform.forward * _input.y;

        direction.Normalize();
        
        Rotation(direction);
        
        Vector3 newPosition = _rigidbody.position + direction * (_speed * Time.fixedDeltaTime);
        float moveDistance = Vector3.Distance(_rigidbody.position, newPosition);
        if (!Physics.Raycast(_rigidbody.position, direction, moveDistance, obstacleMask))
        {
            _rigidbody.MovePosition(newPosition);
        }
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
