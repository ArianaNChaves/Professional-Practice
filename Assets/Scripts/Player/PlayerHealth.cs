using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private UnityEvent OnDeath;
    [SerializeField] private PlayerSO playerData;
    
    private float _health;

    private void Awake()
    {
        _health = playerData.MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
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
