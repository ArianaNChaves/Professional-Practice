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
    
    [Header("Spawn Settings")]
    [SerializeField] private Vector2 startToCatchBetween;
    [SerializeField] private Vector2 spawnRateBetween;
    
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float AttackRate { get => attackRate; set => attackRate = value; }
    public Vector2 StartToCatchBetween { get => startToCatchBetween; set => startToCatchBetween = value; }
    public Vector2 SpawnRate { get => spawnRateBetween; set => spawnRateBetween = value; }
}
