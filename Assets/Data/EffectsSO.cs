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
    
    [Header("Music Settings")]
    [SerializeField] private int initialTime ;
    [SerializeField] private int secondTime;
    [SerializeField] private int lastTime;

    
    public float ShakeDuration { get => shakeDuration; set => shakeDuration = value; }
    public float ShakeMagnitude { get => shakeMagnitude; set => shakeMagnitude = value; }
    public float ShakeFrequency { get => shakeFrequency; set => shakeFrequency = value; }
    
    public int InitialTime { get => initialTime;}
    public int SecondTime { get => secondTime;}
    public int LastTime { get => lastTime;}

}
