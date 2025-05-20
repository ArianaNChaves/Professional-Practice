using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCondition : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    private int enemyDeathCount = 0;
    private const int REQUIRED_DEATHS = 12;

    private void Start()
    {
        playerHealth.OnDeath.AddListener(LoseGame);
        PlayerHealth.OnHealthChanged += OnPlayerHealthChanged;
        Enemy.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDestroy()
    {
        playerHealth.OnDeath.RemoveListener(LoseGame);
        PlayerHealth.OnHealthChanged -= OnPlayerHealthChanged;
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

    private void OnPlayerHealthChanged(float health)
    {
        Debug.Log(health);
        playerHealthText.text = health.ToString("F1");
    }

    private void LoseGame()
    {
        SceneManager.LoadScene("Credits");
    }
}
