using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string playSceneName;
    [SerializeField] private string continueSceneName = "Game";
    [SerializeField] private string creditsSceneName;
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private TextMeshProUGUI playButtonText;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private RectTransform menuButtonsRoot;
    [SerializeField] private float menuYWithSave;

    [Header("Sliders")]
    [SerializeField] private Slider volumeSlider;

    [Header("Display Settings")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown maxFramerateDropdown;
    [SerializeField] private int[] maxFramerateOptions = { -1, 30, 60, 120, 144, 240 };

    [Header("Navigation")]
    [SerializeField] private Selectable firstSelected;
    [SerializeField] private Selectable settingsFirstSelected;
    [SerializeField] private bool keepSelection = true;

    private const string ResolutionWidthKey = "Settings.ResolutionWidth";
    private const string ResolutionHeightKey = "Settings.ResolutionHeight";
    private const string MaxFramerateKey = "Settings.MaxFramerate";

    private readonly List<Resolution> availableResolutions = new List<Resolution>();
    private GameObject lastSelected;
    private float menuDefaultY;

    private void OnEnable()
    {
        if (volumeSlider != null) volumeSlider.onValueChanged.AddListener(SetGeneralVolume);
        if (resolutionDropdown != null) resolutionDropdown.onValueChanged.AddListener(SetResolution);
        if (maxFramerateDropdown != null) maxFramerateDropdown.onValueChanged.AddListener(SetMaxFramerate);
    }

    private void OnDisable()
    {
        if (volumeSlider != null) volumeSlider.onValueChanged.RemoveListener(SetGeneralVolume);
        if (resolutionDropdown != null) resolutionDropdown.onValueChanged.RemoveListener(SetResolution);
        if (maxFramerateDropdown != null) maxFramerateDropdown.onValueChanged.RemoveListener(SetMaxFramerate);
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioManager audioManager = AudioManager.Instance;
        if (audioManager != null)
        {
            audioManager.PlayMusic("Main Menu");
            if (volumeSlider != null)
            {
                audioManager.MasterVolume(volumeSlider.value);
            }
        }
        if (menuButtonsRoot != null) menuDefaultY = menuButtonsRoot.anchoredPosition.y;
        ConfigurePlayButton();
        ConfigureNewGameButton();
        UpdatePlayButtonText();
        InitializeDisplaySettings();
        StartCoroutine(SelectDefaultNextFrame());
    }

    private void ConfigurePlayButton()
    {
        if (firstSelected == null) return;

        Button button = firstSelected.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogWarning("First Selected does not have a Button component.");
            return;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(PlayGame);
    }

    private void ConfigureNewGameButton()
    {
        if (newGameButton == null) return;

        Button button = newGameButton.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogWarning("New Game Button does not have a Button component.");
            return;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(NewGame);
    }

    private void UpdatePlayButtonText()
    {
        TextMeshProUGUI label = playButtonText;
        if (label == null && firstSelected != null)
        {
            label = firstSelected.GetComponentInChildren<TextMeshProUGUI>();
        }

        bool hasSavedGame = Timer.HasSavedTimer();
        if (label != null)
        {
            label.text = hasSavedGame ? "Continue" : "Play";
        }

        if (newGameButton != null)
        {
            newGameButton.SetActive(hasSavedGame);
        }

        if (menuButtonsRoot != null)
        {
            Vector2 anchoredPosition = menuButtonsRoot.anchoredPosition;
            anchoredPosition.y = hasSavedGame ? menuYWithSave : menuDefaultY;
            menuButtonsRoot.anchoredPosition = anchoredPosition;
            ConfigureMenuButtonNavigation();
        }
    }

    private void ConfigureMenuButtonNavigation()
    {
        List<Selectable> selectables = new List<Selectable>();

        if (firstSelected != null && firstSelected.gameObject.activeInHierarchy)
        {
            selectables.Add(firstSelected);
        }

        if (newGameButton != null && newGameButton.activeInHierarchy)
        {
            Selectable newGameSelectable = newGameButton.GetComponent<Selectable>();
            if (newGameSelectable != null)
            {
                selectables.Add(newGameSelectable);
            }
        }

        selectables.AddRange(menuButtonsRoot.GetComponentsInChildren<Selectable>(false));
        if (selectables.Count == 0) return;

        for (int i = 0; i < selectables.Count; i++)
        {
            Navigation navigation = selectables[i].navigation;
            navigation.mode = Navigation.Mode.Explicit;
            navigation.selectOnUp = i > 0 ? selectables[i - 1] : selectables[selectables.Count - 1];
            navigation.selectOnDown = i < selectables.Count - 1 ? selectables[i + 1] : selectables[0];
            navigation.selectOnLeft = null;
            navigation.selectOnRight = null;
            selectables[i].navigation = navigation;
        }
    }

    private void InitializeDisplaySettings()
    {
        PopulateResolutionDropdown();
        PopulateMaxFramerateDropdown();
    }

    private void PopulateResolutionDropdown()
    {
        if (resolutionDropdown == null) return;

        availableResolutions.Clear();
        resolutionDropdown.ClearOptions();

        int savedWidth = PlayerPrefs.GetInt(ResolutionWidthKey, Screen.currentResolution.width);
        int savedHeight = PlayerPrefs.GetInt(ResolutionHeightKey, Screen.currentResolution.height);
        int selectedIndex = 0;
        List<string> options = new List<string>();

        foreach (Resolution resolution in Screen.resolutions)
        {
            bool alreadyAdded = false;
            for (int i = 0; i < availableResolutions.Count; i++)
            {
                if (availableResolutions[i].width == resolution.width && availableResolutions[i].height == resolution.height)
                {
                    alreadyAdded = true;
                    break;
                }
            }

            if (alreadyAdded) continue;

            if (resolution.width == savedWidth && resolution.height == savedHeight)
            {
                selectedIndex = availableResolutions.Count;
            }

            availableResolutions.Add(resolution);
            options.Add($"{resolution.width} x {resolution.height}");
        }

        if (availableResolutions.Count == 0)
        {
            Resolution currentResolution = Screen.currentResolution;
            availableResolutions.Add(currentResolution);
            options.Add($"{currentResolution.width} x {currentResolution.height}");
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.SetValueWithoutNotify(selectedIndex);
        resolutionDropdown.RefreshShownValue();
    }

    private void PopulateMaxFramerateDropdown()
    {
        if (maxFramerateDropdown == null) return;

        maxFramerateDropdown.ClearOptions();
        int savedFramerate = PlayerPrefs.GetInt(MaxFramerateKey, Application.targetFrameRate);
        int selectedIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < maxFramerateOptions.Length; i++)
        {
            int framerate = maxFramerateOptions[i];
            options.Add(framerate <= 0 ? "Unlimited" : framerate.ToString());

            if (framerate == savedFramerate)
            {
                selectedIndex = i;
            }
        }

        maxFramerateDropdown.AddOptions(options);
        maxFramerateDropdown.SetValueWithoutNotify(selectedIndex);
        maxFramerateDropdown.RefreshShownValue();
        SetMaxFramerate(selectedIndex);
    }

    private void Update()
    {
        if (!keepSelection || EventSystem.current == null) return;

        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (selected != null && selected.activeInHierarchy)
        {
            lastSelected = selected;
            return;
        }

        SelectCurrentDefault();
    }

    private IEnumerator SelectDefaultNextFrame()
    {
        yield return null;
        SelectCurrentDefault();
    }

    private void SelectCurrentDefault()
    {
        if (settingsPanel != null && settingsPanel.activeInHierarchy)
        {
            Select(settingsFirstSelected != null ? settingsFirstSelected : volumeSlider);
            return;
        }

        if (lastSelected != null && lastSelected.activeInHierarchy)
        {
            Select(lastSelected);
            return;
        }

        if (firstSelected != null)
        {
            Select(firstSelected);
            return;
        }

        if (EventSystem.current != null && EventSystem.current.firstSelectedGameObject != null)
        {
            Select(EventSystem.current.firstSelectedGameObject);
        }
    }

    private void Select(Selectable selectable)
    {
        if (selectable == null) return;
        Select(selectable.gameObject);
    }

    private void Select(GameObject selected)
    {
        if (EventSystem.current == null || selected == null || !selected.activeInHierarchy) return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selected);
        lastSelected = selected;
    }

    public void PlayGame()
    {
        string sceneName = Timer.HasSavedTimer() ? continueSceneName : playSceneName;
        Debug.Log($"Play button clicked. Loading scene: {sceneName}");
        LoadScene(sceneName);
    }

    public void NewGame()
    {
        Timer.ClearSavedTimer();
        Debug.Log($"New Game button clicked. Loading scene: {playSceneName}");
        LoadScene(playSceneName);
    }

    private void LoadScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("MenuManager cannot load scene because the scene name is empty.");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
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

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= availableResolutions.Count) return;

        Resolution resolution = availableResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        PlayerPrefs.SetInt(ResolutionWidthKey, resolution.width);
        PlayerPrefs.SetInt(ResolutionHeightKey, resolution.height);
        PlayerPrefs.Save();
    }

    public void SetMaxFramerate(int framerateIndex)
    {
        if (framerateIndex < 0 || framerateIndex >= maxFramerateOptions.Length) return;

        int framerate = maxFramerateOptions[framerateIndex];
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = framerate;
        PlayerPrefs.SetInt(MaxFramerateKey, framerate);
        PlayerPrefs.Save();
    }

    public void OpenSettings()
    {
        if (settingsPanel == null) return;

        bool isOpen = !settingsPanel.activeInHierarchy;
        settingsPanel.SetActive(isOpen);

        if (isOpen)
        {
            Select(settingsFirstSelected != null ? settingsFirstSelected : volumeSlider);
            return;
        }

        Select(firstSelected != null ? firstSelected.gameObject : lastSelected);
    }
}
