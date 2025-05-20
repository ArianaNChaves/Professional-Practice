using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public UnityEvent OnDeath;
    public static event Action<float> OnHealthChanged;
    [SerializeField] private PlayerSO playerData;
    
    private float _health;

    private void Start()
    {
        OnHealthChanged?.Invoke(_health);
    }

    private void Awake()
    {
        _health = playerData.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health);
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
