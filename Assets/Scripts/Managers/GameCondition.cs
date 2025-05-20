using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCondition : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth.OnDeath.AddListener(LoseGame);
    }

    private void LoseGame()
    {
        SceneManager.LoadScene("Credits");
    }
}
