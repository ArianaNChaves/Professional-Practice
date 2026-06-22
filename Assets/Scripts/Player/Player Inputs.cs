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
        MessageSystem.Publish(new PauseRequestedEvent());
    }
    private void OnInteract(InputAction.CallbackContext context)
    {
        MessageSystem.Publish(new InteractRequestedEvent());
    }
    private void OnCheatsUsed(InputAction.CallbackContext context)
    {
        MessageSystem.Publish(new CheatsRequestedEvent());
    }
}
