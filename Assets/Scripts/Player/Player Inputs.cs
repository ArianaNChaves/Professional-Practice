using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public static event Action OnPauseGame;
    public static event Action Oninteraction;
    public static event Action OnCheats;
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
        _playerInputActions.PlayerMaps.Pause.started += OnPause;
        _playerInputActions.PlayerMaps.Interact.started += OnInteract;
        _playerInputActions.PlayerMaps.Cheats.started += OnCheatsUsed;

    }
    
    private void OnDisable()
    {
        _playerInputActions.PlayerMaps.Disable();
        _playerInputActions.PlayerMaps.Movement.performed -= OnMovement;
        _playerInputActions.PlayerMaps.Movement.canceled -= OnMovement;
        _playerInputActions.PlayerMaps.Pause.started -= OnPause;
        _playerInputActions.PlayerMaps.Interact.started -= OnInteract;
        _playerInputActions.PlayerMaps.Cheats.started -= OnCheatsUsed;
    }
    
    private void OnMovement(InputAction.CallbackContext context)
    {
        playerMovement.OnMovement(context.ReadValue<Vector2>());
    }
    private void OnPause(InputAction.CallbackContext context)
    {
        OnPauseGame?.Invoke();
    }
    private void OnInteract(InputAction.CallbackContext context)
    {
        Oninteraction?.Invoke();
    }
    private void OnCheatsUsed(InputAction.CallbackContext context)
    {
        OnCheats?.Invoke();
    }
}
