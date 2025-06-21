using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effects Data", menuName = "ScriptableObjects/Effects Data")]

public class EffectsSO : ScriptableObject
{
    [Header("Camera Settings")]
    [SerializeField] private float shakeDuration ;
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeFrequency;

    
    public float ShakeDuration { get => shakeDuration; set => shakeDuration = value; }
    public float ShakeMagnitude { get => shakeMagnitude; set => shakeMagnitude = value; }
    public float ShakeFrequency { get => shakeFrequency; set => shakeFrequency = value; }

}
