using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; 
    [SerializeField] private string targetSceneName; 
    private bool isPaused = false;

    private void OnEnable()
    {
        MessageSystem.Subscribe<PauseRequestedEvent>(OnPauseRequested);
    }
    private void OnDisable()
    {
        MessageSystem.Unsubscribe<PauseRequestedEvent>(OnPauseRequested);
    }

    private void OnPauseRequested(PauseRequestedEvent pauseRequestedEvent)
    {
        TogglePause();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
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
