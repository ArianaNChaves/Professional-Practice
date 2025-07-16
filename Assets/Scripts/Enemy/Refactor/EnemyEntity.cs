using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour, IDamageable, IPooleable
{
    //Ejemplo de un enemigo
    [SerializeField] private EnemySO enemyData; 
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody enemyRigidbody;
    
    protected EnemyMachine EnemyMachine;
    protected float MoveSpeed;
    protected float Health;
    protected float Damage;
    protected float AttackRange;
    protected float AttackRate;
    
    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void ReturnObjectToPool()
    {
        throw new System.NotImplementedException();
    }
}
