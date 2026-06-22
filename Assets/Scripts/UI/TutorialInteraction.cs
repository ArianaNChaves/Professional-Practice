using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialInteraction : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        MessageSystem.Subscribe<InteractRequestedEvent>(OnInteractRequested);
    }
    
    private void OnDisable()
    {
        MessageSystem.Unsubscribe<InteractRequestedEvent>(OnInteractRequested);
    }

    private void OnInteractRequested(InteractRequestedEvent interactRequestedEvent)
    {
        PlayGame();
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
