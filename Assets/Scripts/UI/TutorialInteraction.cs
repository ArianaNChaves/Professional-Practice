using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialInteraction : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerInputs.Oninteraction += PlayGame;
    }
    
    private void OnDisable()
    {
        PlayerInputs.Oninteraction -= PlayGame;
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
