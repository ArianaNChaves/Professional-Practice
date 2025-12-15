using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject _lastTarget;
    private int _enemiesSpawned = 0;
    private bool _enemiesCanSpawn = true;
    
    private void OnEnable()
    {
        DeprecatedEnemy.OnSpawn += EnemySpawn;
    }
    private void OnDisable()
    {
        DeprecatedEnemy.OnSpawn -= EnemySpawn;
    }

    private void EnemySpawn(DeprecatedEnemy newDeprecatedEnemy)
    {
        // _enemiesSpawned++;
        newDeprecatedEnemy.SetTargetPlayer(player); 
    }
    
    public bool EnemiesCanSpawn()
    {
        return _enemiesCanSpawn;
    }

    public void StopSpawnEnemies()
    {
        _enemiesCanSpawn = false;
    }
}
