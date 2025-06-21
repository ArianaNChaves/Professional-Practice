using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private EffectsSO cameraEffectData;
    [SerializeField] private CinemachineVirtualCamera virtualPlayerCamera;
    private CinemachineBasicMultiChannelPerlin _cameraShake;
    
    private Vector3 _originalPosition;
    
    private void Awake()
    {
        _cameraShake = virtualPlayerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void OnEnable()
    {
        BallCollisions.OnBallCrash += Shake;
    }
    
    private void OnDisable()
    {
        BallCollisions.OnBallCrash -= Shake;
    }
    private void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }
    
    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;
        float duration = cameraEffectData.ShakeDuration;

        float startAmplitude = cameraEffectData.ShakeMagnitude;
        float startFrequency = cameraEffectData.ShakeFrequency;

        _cameraShake.m_AmplitudeGain = startAmplitude;
        _cameraShake.m_FrequencyGain = startFrequency;

        while (elapsed < duration)
        {
            float percent = elapsed / duration;

            _cameraShake.m_AmplitudeGain = Mathf.Lerp(startAmplitude, 0f, percent);
            _cameraShake.m_FrequencyGain = Mathf.Lerp(startFrequency, 0f, percent);

            elapsed += Time.deltaTime;
            yield return null;
        }

        _cameraShake.m_AmplitudeGain = 0f;
        _cameraShake.m_FrequencyGain = 0f;
    }
}
