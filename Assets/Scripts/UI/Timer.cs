using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private int initialTimeInMinutes;
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private TextMeshProUGUI secondsText;
    [SerializeField] private TextMeshProUGUI minutesText;
    
    private float _timeLeft;

    private void Start()
    {
        _timeLeft = initialTimeInMinutes * 60f;
        UpdateTime();
    }

    private void Update()
    {
        UpdateTime();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += DiscountTime;
        PlayerHealth.OnPlayerHit += AddTime;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= DiscountTime;
        PlayerHealth.OnPlayerHit -= AddTime;
    }
    
    private void DiscountTime()
    {
        
        _timeLeft -= enemyData.EnemyTimeValue;
    }
    
    private void AddTime(float health)
    {
        _timeLeft += playerData.PlayerHitTimeValue;
    }

    private void UpdateTime()
    {
        ConvertTime();
        
        _timeLeft -= Time.deltaTime;
        if (_timeLeft < 0f) _timeLeft = 0f;
    }

    private void ConvertTime()
    {
        int minutes = Mathf.FloorToInt(_timeLeft / 60f);
        int seconds = Mathf.FloorToInt(_timeLeft % 60f);

        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");
    }
    
}
