using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum ControlDevice
{
    Keyboard,
    Gamepad
}
public class ControlDetection : MonoBehaviour
{
    public static event Action<ControlDevice> OnControlChange;
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
        Debug.Log($"Cambio de control {newDevice.currentControlScheme}");
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
        OnControlChange?.Invoke(_currentDevice);
        
    }

}


