using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCondition : MonoBehaviour
{
    private void WinGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    private void LoseGame()
    {
        SceneManager.LoadScene("Credits");
    }
}
