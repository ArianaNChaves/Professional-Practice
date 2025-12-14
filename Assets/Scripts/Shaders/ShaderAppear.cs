using System.Collections;
using System.Collections.Generic;
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
        BallCollisions.OnBallWallCrash += PlayShader;
    }

    private void OnDisable()
    {
        BallCollisions.OnBallWallCrash -= PlayShader;
    }

    private void PlayShader(Vector3 position)
    {
        transform.position = position;
        _visualEffect.Play();
    }
}
