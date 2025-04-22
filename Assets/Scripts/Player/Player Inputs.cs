using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    
    private PlayerInputActions _playerInputActions;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();

    }
    private void OnEnable()
    {
        _playerInputActions.PlayerMaps.Enable();
        _playerInputActions.PlayerMaps.Movement.performed += OnMovement;
        _playerInputActions.PlayerMaps.Movement.canceled += OnMovement;
    }
    
    private void OnDisable()
    {
        _playerInputActions.PlayerMaps.Disable();
        _playerInputActions.PlayerMaps.Movement.performed -= OnMovement;
        _playerInputActions.PlayerMaps.Movement.canceled -= OnMovement;
    }
    
    private void OnMovement(InputAction.CallbackContext context)
    {
        playerMovement.OnMovement(context.ReadValue<Vector2>());
    }
}
