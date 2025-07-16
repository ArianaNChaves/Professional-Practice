using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyFSM : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private Transform player;
    
    private NavMeshAgent _agent;
    private Animator _anim;
    private EnemyState _currentEnemyState;
    private float _health;
    private float _maxHealth;
    private bool _isDeath = false;
    private float _damage;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        // _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _maxHealth = enemyData.MaxHealth;
        _health = _maxHealth;
        _damage = enemyData.Damage;
        _currentEnemyState = new Initial(this.gameObject, _agent, _anim, player);
    }
    private void Update()
    {
        _currentEnemyState = _currentEnemyState.Process();
        
        if (_currentEnemyState.name == EnemyState.STATE.ATTACK) //todo Sacar esto del update
        {
            playerData.PlayerDamageable.TakeDamage(_damage);
        }
    }
    public void Instantiate(Transform target)
    {
        player = target;
    }
    private void Die()
    {
        _isDeath = true;
        _currentEnemyState = new Wait(this.gameObject, _agent, _anim, player);
        //_anim.SetTrigger(Die); suponiendo que dura 2 segundos la animacion
        //si tengo que sacarlo de algun metodo o array o mandar evento es ahora
        Destroy(gameObject, 2.0f);

    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0 && !_isDeath)
        {
            Die();
        }
    }
}
