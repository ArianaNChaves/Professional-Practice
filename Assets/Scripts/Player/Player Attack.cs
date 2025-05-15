using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    public UnityEvent<bool> onSpinning;
    
    [SerializeField] private GameObject ball;
    [SerializeField] private float spinTorque;

    private bool _isSpinning = false;
    private bool _isSpinningToRight;
    private Rigidbody _rigidbody;
    private Quaternion _initialRotation;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _initialRotation = transform.rotation;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeToSpinningMode();
            _isSpinningToRight = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeToSpinningMode();
            _isSpinningToRight = true;
        }

        SpinningLogic();
    }
    

    private void ChangeToSpinningMode()
    {
        _isSpinning = !_isSpinning;
        _rigidbody.isKinematic = !_isSpinning;
        if (!_isSpinning)
        {
            WhenStopSpinning(); 
        }
    }

    private void SpinningLogic()
    {
        if (!_isSpinning) return;
        transform.LookAt(ball.transform);
        onSpinning?.Invoke(_isSpinning);
        if (_isSpinningToRight)
        {
            transform.Rotate(Vector3.up * (-spinTorque * Time.deltaTime));
        }
        else
        {
            transform.Rotate(Vector3.up * (spinTorque * Time.deltaTime));
        }
    }

    private void WhenStopSpinning()
    {
        transform.rotation = _initialRotation;
    }
    
    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}
