using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour, IPooleable
{
    //Ejemplo de un enemigo
    [SerializeField] private EnemySO enemyData; 
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody enemyRigidbody;
    [SerializeField] private Collider enemyCollider;
    
    protected float MoveSpeed;
    protected float Damage;
    protected float AttackRange;
    protected float AttackRate;
    
    public void ReturnObjectToPool()
    {
        throw new System.NotImplementedException();
    }
}
