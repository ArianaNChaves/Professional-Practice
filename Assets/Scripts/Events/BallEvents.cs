using UnityEngine;

public readonly struct BallCrashEvent
{
}

public readonly struct BallWallCrashEvent
{
    public BallWallCrashEvent(Vector3 position)
    {
        Position = position;
    }

    public Vector3 Position { get; }
}
