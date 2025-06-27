using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Needed for Any() check

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private Transform spawnPoint;
    private Vector2 _spawnRate;
    private float _spawnTimer;
    
    private void Start()
    {
        _spawnRate = enemyData.SpawnRate;
        RandomSpawnTime();
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f)
        {
            SpawnHandler();
            RandomSpawnTime();
        }
    }

    private void RandomSpawnTime()
    {
        _spawnTimer = Random.Range(_spawnRate.x, _spawnRate.y);
    }

    private void SpawnHandler()
    {
        Enemy newEnemy = PoolManager.Instance.Get<Enemy>();

        if (!newEnemy)
        {
            Debug.LogError("New enemy es nulo - EnemySpawner.cs - SpawnHandler");
            return;
        }
        newEnemy.transform.position = transform.position;
        newEnemy.transform.rotation = transform.rotation;
        newEnemy.gameObject.SetActive(true);
        newEnemy.SetSpawnPoint(spawnPoint);
        newEnemy.Activate();
    }
}
