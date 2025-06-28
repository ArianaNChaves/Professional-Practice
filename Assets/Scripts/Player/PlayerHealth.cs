using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public UnityEvent OnDeath;
    public static event Action<float> OnPlayerHit;
    [SerializeField] private PlayerSO playerData;
    
    private float _health;

    private void Awake()
    {
        _health = playerData.MaxHealth;
    }
    private void Start()
    {
        OnPlayerHit?.Invoke(_health);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnPlayerHit?.Invoke(_health);
        if (_health <= 0)
        {
            _health = 0;
            Death();
        }
    }

    private void Death()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }
    
}
