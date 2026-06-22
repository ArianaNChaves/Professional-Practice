using UnityEngine;

public class ChangeDeviceUi : MonoBehaviour
{
    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] mando;

    private void OnEnable()
    {
        MessageSystem.Subscribe<ControlDeviceChangedEvent>(OnControlDeviceChanged);
    }
    private void OnDisable()
    {
        MessageSystem.Unsubscribe<ControlDeviceChangedEvent>(OnControlDeviceChanged);
    }

    private void OnControlDeviceChanged(ControlDeviceChangedEvent controlDeviceChangedEvent)
    {
        ChangeUiImage(controlDeviceChangedEvent.Device);
    }

    private void ChangeUiImage(ControlDevice device)
    {
        switch (device)
        {
            case ControlDevice.Keyboard:
                foreach (GameObject mandoObject in mando)
                {
                    mandoObject.SetActive(false);
                }
                foreach (GameObject keyboardObject in keyboard)
                {
                    keyboardObject.SetActive(true);
                }
                break;
            case ControlDevice.Gamepad:
                foreach (GameObject keyboardObject in keyboard)
                {
                    keyboardObject.SetActive(false);
                }
                foreach (GameObject mandoObject in mando)
                {
                    mandoObject.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
}

