using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObjects/Player Data")]
public class PlayerSO : ScriptableObject
{
    [Header("Stats Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    
    [Header("Time Settings")] 
    [SerializeField] private float playerHitTimeValue;
    
    private IDamageable _playerDamageable;
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Damage { get => damage; set => damage = value; }
    
    public float PlayerHitTimeValue { get => playerHitTimeValue; set => playerHitTimeValue = value; }
    public IDamageable PlayerDamageable { get => _playerDamageable; set => _playerDamageable = value; }


}
