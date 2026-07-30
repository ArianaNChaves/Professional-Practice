using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; 
    [SerializeField] private string targetSceneName; 
    [SerializeField] private TextMeshProUGUI saveFeedbackText;
    [SerializeField] private float saveFeedbackDuration = 2f;

    private bool isPaused = false;
    private Coroutine saveFeedbackCoroutine;

    private void Start()
    {
        if (saveFeedbackText != null)
        {
            saveFeedbackText.gameObject.SetActive(false);
        }
    }

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

    public void SaveGame()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer == null) return;

        timer.SaveTimer();
        ShowSaveFeedback();
    }

    private void ShowSaveFeedback()
    {
        if (saveFeedbackText == null) return;

        if (saveFeedbackCoroutine != null)
        {
            StopCoroutine(saveFeedbackCoroutine);
        }

        saveFeedbackCoroutine = StartCoroutine(ShowSaveFeedbackCoroutine());
    }

    private IEnumerator ShowSaveFeedbackCoroutine()
    {
        saveFeedbackText.text = "Game saved";
        saveFeedbackText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(saveFeedbackDuration);

        saveFeedbackText.gameObject.SetActive(false);
        saveFeedbackCoroutine = null;
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
