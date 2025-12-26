using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static event Action OnTimeReached;
    
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private EffectsSO effectsData;
    [SerializeField] private TextMeshProUGUI secondsText;
    [SerializeField] private TextMeshProUGUI minutesText;
    [SerializeField] private Color discountColor;
    [SerializeField] private Color addColor;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 10f;

    private float _timeLeft;
    private Image _panel;
    private Color _baseColor;
    private Vector3 _basePos;
    private Coroutine _shakeCoroutine;
    private int _initialTime;
    private int _lastTime;
    private int _secondTime;
    private int _lastMinute;
    private bool _godMode;
    private bool _winGame = false;
    private string _currentTrack;
    private AudioManager _audioManager;
    
    private void Start()
    {
        _initialTime = effectsData.InitialTime;
        _lastTime = effectsData.LastTime;
        _secondTime = effectsData.SecondTime;
        _lastMinute = _initialTime;
        _audioManager = AudioManager.Instance;
        
        _timeLeft = _initialTime * 60f;
        UpdateTime();

        _panel = GetComponent<Image>();
        _baseColor = _panel.color;
        _basePos = _panel.rectTransform.localPosition;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        if (_timeLeft < 0f)
        {
            _timeLeft = 0f;
            _winGame = true;
            if (!_godMode)
            {
                OnTimeReached?.Invoke();
            }
        }
        UpdateTime();
    }
    
    private void OnEnable()
    {
        Enemy.OnEnemyDeath += OnDiscountTime;
        PlayerHealth.OnPlayerHit += OnAddTime;
        Cheats.OnGodMode += GodMode;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= OnDiscountTime;
        PlayerHealth.OnPlayerHit -= OnAddTime;
        Cheats.OnGodMode -= GodMode;
    }

    private void OnDiscountTime()
    {
        if (_winGame) return;
        _timeLeft -= enemyData.EnemyTimeValue;
        StopAllCoroutines();
        StartCoroutine(ShakeDiscount());
    }

    private void OnAddTime()
    {
        if (_godMode || _winGame) return;
        _timeLeft += playerData.PlayerHitTimeValue;
        StopAllCoroutines();
        StartCoroutine(ShakeAdd());
    }
    
    private void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(_timeLeft / 60f);
        int seconds = Mathf.FloorToInt(_timeLeft % 60f);
        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");
        if (minutes == _lastMinute) return;
        _lastMinute = minutes;
        PlayMusicWhile(minutes);
    }

    private void PlayMusicWhile(int minutes)
    {
        string nextTrack;
        if (minutes > _secondTime)
            nextTrack = "Initial Time";
        else if (minutes > _lastTime)
            nextTrack = "Second Time";
        else
            nextTrack = "Last Time";
        if (nextTrack == _currentTrack) 
            return;
        _currentTrack = nextTrack;
        _audioManager.PlayMusic(_currentTrack);
    }

    private IEnumerator ShakeAdd()
    {
        _panel.color = addColor;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float offsetX = UnityEngine.Random.Range(-shakeStrength, shakeStrength);
            _panel.rectTransform.localPosition = _basePos + new Vector3(offsetX, 0f, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _panel.rectTransform.localPosition = _basePos;
        _panel.color = _baseColor;
    }

    private IEnumerator ShakeDiscount()
    {
        _panel.color = discountColor;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float offsetY = UnityEngine.Random.Range(-shakeStrength, shakeStrength);
            _panel.rectTransform.localPosition = _basePos + new Vector3(0f, offsetY, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _panel.rectTransform.localPosition = _basePos;
        _panel.color = _baseColor;
    }

    private void GodMode()
    {
        _godMode = !_godMode;
    }
}
