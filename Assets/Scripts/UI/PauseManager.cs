using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Required for scene management

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; 
    [SerializeField] private string targetSceneName; 
    private bool isPaused = false;


    private void OnEnable()
    {
        PlayerInputs.OnPauseGame += TogglePause;
    }
    private void OnDisable()
    {
        PlayerInputs.OnPauseGame += TogglePause;
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        if (pausePanel != null) pausePanel.SetActive(isPaused);
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }

    public void LoadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(targetSceneName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
