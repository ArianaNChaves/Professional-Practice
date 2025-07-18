using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "ScriptableObjects/Enemy Data")]

public class EnemySO : ScriptableObject
{
    [Header("Stats Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    [SerializeField] private float rotationSpeed;
    
    [Header("Life cycle Settings")]
    [SerializeField] private Vector2 spawnRateBetween;
    [SerializeField] private float timeToDespawn;
    [SerializeField] private float deathForce;

    [Header("Time Settings")] 
    [SerializeField] private float enemyTimeValue;
    
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float AttackRate { get => attackRate; set => attackRate = value; }
    public Vector2 SpawnRate { get => spawnRateBetween; set => spawnRateBetween = value; }
    public float EnemyTimeValue { get => enemyTimeValue; set => enemyTimeValue = value; }
    public float TimeToDespawn { get => timeToDespawn; set => timeToDespawn = value; }
    public float DeathForce { get => deathForce; set => deathForce = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
}
