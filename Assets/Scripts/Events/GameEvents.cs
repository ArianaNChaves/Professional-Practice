public readonly struct PauseRequestedEvent
{
}

public readonly struct InteractRequestedEvent
{
}

public readonly struct CheatsRequestedEvent
{
}

public enum ControlDevice
{
    Keyboard,
    Gamepad
}

public readonly struct ControlDeviceChangedEvent
{
    public ControlDeviceChangedEvent(ControlDevice device)
    {
        Device = device;
    }

    public ControlDevice Device { get; }
}

public readonly struct GodModeChangedEvent
{
    public GodModeChangedEvent(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    public bool IsEnabled { get; }
}

public readonly struct PlayerHitEvent
{
}
