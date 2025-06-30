using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string playSceneName;
    [SerializeField] private string creditsSceneName;
    [SerializeField] private string mainMenuSceneName;

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Main Menu");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(playSceneName);
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
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
