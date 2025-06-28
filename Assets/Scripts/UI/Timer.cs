using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private int initialTimeInMinutes;
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private PlayerSO playerData;
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

    private void Start()
    {
        _timeLeft = initialTimeInMinutes * 60f;
        UpdateTimeDisplay();

        _panel = GetComponent<Image>();
        _baseColor = _panel.color;
        _basePos = _panel.rectTransform.localPosition;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        if (_timeLeft < 0f) _timeLeft = 0f;
        UpdateTimeDisplay();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += OnDiscountTime;
        PlayerHealth.OnPlayerHit += OnAddTime;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= OnDiscountTime;
        PlayerHealth.OnPlayerHit -= OnAddTime;
    }

    private void OnDiscountTime()
    {
        _timeLeft -= enemyData.EnemyTimeValue;
        StopAllCoroutines();
        StartCoroutine(ShakeDiscount());
    }

    private void OnAddTime()
    {
        _timeLeft += playerData.PlayerHitTimeValue;
        StopAllCoroutines();
        StartCoroutine(ShakeAdd());
    }

    private void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(_timeLeft / 60f);
        int seconds = Mathf.FloorToInt(_timeLeft % 60f);
        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");
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
}
