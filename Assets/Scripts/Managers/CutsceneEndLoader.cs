using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneEndLoader : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private string nextSceneName = "Credits";

    private void Awake()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
    }

    private void OnEnable()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped += OnCutsceneStopped;
        }
    }

    private void OnDisable()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnCutsceneStopped;
        }
    }

    private void OnCutsceneStopped(PlayableDirector director)
    {
        if (string.IsNullOrWhiteSpace(nextSceneName))
        {
            Debug.LogError("CutsceneEndLoader cannot load scene because Next Scene Name is empty.");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }
}
