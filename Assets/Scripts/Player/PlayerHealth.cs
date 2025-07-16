using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour, IDamageable
{ 
    [SerializeField] private PlayerSO playerData;
    public static event Action OnPlayerHit;
    
    public void TakeDamage(float damage)
    {
        AudioManager.Instance.PlayEffect("Player Hit");
        OnPlayerHit?.Invoke();
    }
}
