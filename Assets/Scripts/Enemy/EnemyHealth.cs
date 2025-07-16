using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : EnemyEntity, IDamageable
{
    public static event Action OnEnemyDeath;

    [SerializeField] private EnemySO enemyData; 
    
    private float _health;
    private float _maxHealth;

    private void Start()
    {
        _maxHealth = enemyData.MaxHealth;
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            CurrentState = new EnemyDeath(this);
        }
    }
}
