using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public UnityEvent OnDeath;
    public static event Action OnPlayerHit;

    public void TakeDamage(float damage)
    {
        OnPlayerHit?.Invoke();
    }
    
}
