using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Data", menuName = "ScriptableObjects/Audio Data")]

public class AudioSO : ScriptableObject
{
    [Header("Audio Settings")]
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;
    [SerializeField] private float uiVolume;
    [SerializeField] private float masterVolume;
    
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }
    public float SFXVolume { get => sfxVolume; set => sfxVolume = value; }
    public float UIVolume { get => uiVolume; set => uiVolume = value; }
    public float MasterVolume { get => masterVolume; set => masterVolume = value; }

    
}
