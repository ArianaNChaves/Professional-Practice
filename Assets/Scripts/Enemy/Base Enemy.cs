using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected float health;
    protected enum State
    {
        Idle,
        Moving,
        Attacking
    }
    
    private State _currentState;

    private void Start()
    {
        _currentState = State.Idle;  
    }

    protected void ChangeState(State newState)
    {
        _currentState = newState;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }

    protected void Death()
    {
        gameObject.SetActive(false);
    }
    
    
}
