using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // Reference to the pause UI panel
    [SerializeField] private string targetSceneName; // Name of the scene to load
    private bool isPaused = false;

    private void Update()
    {
        // Check for P key press
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        
        // Toggle time scale
        Time.timeScale = isPaused ? 0f : 1f;
        
        // Toggle pause panel visibility
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }
    }

    // Method to resume game (can be called from UI button)
    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }

    // Method to load another scene
    public void LoadScene()
    {
        // Resume time scale before loading new scene
        Time.timeScale = 1f;
        
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("Target scene name is not set!");
        }
    }

    // Method to quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
