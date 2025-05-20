using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCondition : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    private int enemyDeathCount = 0;
    private const int REQUIRED_DEATHS = 12;

    private void Start()
    {
        playerHealth.OnDeath.AddListener(LoseGame);
        Enemy.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath()
    {
        enemyDeathCount++;
        if (enemyDeathCount >= REQUIRED_DEATHS)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    private void LoseGame()
    {
        SceneManager.LoadScene("Credits");
    }
}
