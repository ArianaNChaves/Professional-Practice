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
    
    // private PlayerInputActions _playerInputActions;
    private Vector2 _movement;
    
    private void Awake()
    {
        // _playerInputActions = new PlayerInputActions();
        
    }
    // private void OnEnable()
    // {
    //     _playerInputActions.PlayerMaps.Enable();
    //     _playerInputActions.PlayerMaps.Movement.performed += OnMovement;
    //     _playerInputActions.PlayerMaps.Movement.canceled += OnMovement;
    // }
    //
    // private void OnDisable()
    // {
    //     _playerInputActions.PlayerMaps.Disable();
    //     _playerInputActions.PlayerMaps.Movement.performed -= OnMovement;
    //     _playerInputActions.PlayerMaps.Movement.canceled -= OnMovement;
    // }

    private void Update()
    {
        Debug.Log(_movement);
        
    }
    public void OnMovement(Vector2 context)
    {
        _movement = context;
    }

    


}
