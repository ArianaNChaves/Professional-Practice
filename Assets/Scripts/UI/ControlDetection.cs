using UnityEngine;
using UnityEngine.InputSystem;
public class ControlDetection : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject keyboardImage;
    [SerializeField] private GameObject mandoImage;

    private ControlDevice _currentDevice;

    private void Start()
    {
        OnChangeDevice(playerInput);
    }
    private void OnEnable()
    {
        playerInput.onControlsChanged += OnChangeDevice;
    }
    private void OnDisable()
    {
        playerInput.onControlsChanged -= OnChangeDevice;
    }


    private void OnChangeDevice(PlayerInput newDevice)
    {
        if (newDevice.currentControlScheme == "Keyboard Scheme")
        {
            _currentDevice = ControlDevice.Keyboard;
            mandoImage.SetActive(false);
            keyboardImage.SetActive(true);
        }
        else
        {
            _currentDevice = ControlDevice.Gamepad;
            keyboardImage.SetActive(false);
            mandoImage.SetActive(true);
        }
        MessageSystem.Publish(new ControlDeviceChangedEvent(_currentDevice));
        
    }

}


