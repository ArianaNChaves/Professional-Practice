using UnityEngine;
using UnityEngine.VFX;

public class ShaderAppear : MonoBehaviour
{
    private VisualEffect _visualEffect;
    
    private void Awake()
    {
        _visualEffect = GetComponent<VisualEffect>();
    }
    
    private void OnEnable()
    {
        MessageSystem.Subscribe<BallWallCrashEvent>(OnBallWallCrash);
    }

    private void OnDisable()
    {
        MessageSystem.Unsubscribe<BallWallCrashEvent>(OnBallWallCrash);
    }

    private void OnBallWallCrash(BallWallCrashEvent ballWallCrashEvent)
    {
        PlayShader(ballWallCrashEvent.Position);
    }

    private void PlayShader(Vector3 position)
    {
        transform.position = position;
        _visualEffect.Play();
    }
}
