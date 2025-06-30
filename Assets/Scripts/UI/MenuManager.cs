using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private string playSceneName;
    [SerializeField] private string creditsSceneName;
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private GameObject settingsPanel;
    
    [Header("Sliders")]
    [SerializeField] private Slider volumeSlider;

    private void OnEnable()
    {
        if (volumeSlider == null) return;
        volumeSlider.onValueChanged.AddListener(SetGeneralVolume);
    }

    private void OnDisable()
    {
        if (volumeSlider == null) return;
        volumeSlider.onValueChanged.RemoveListener(SetGeneralVolume);
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioManager.Instance.PlayMusic("Main Menu");
        AudioManager.Instance.MasterVolume(volumeSlider.value);
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

    public void SetGeneralVolume(float value)
    {
        AudioManager.Instance.MasterVolume(value);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }
}
