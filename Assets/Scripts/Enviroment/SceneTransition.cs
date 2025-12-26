using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;
    [SerializeField] private float fadeDuration;
    
    private ColorAdjustments _colorAdjustments;
    private float _timer = 0f;

    private void Awake()
    {
        _colorAdjustments = globalVolume.profile.TryGet(out _colorAdjustments) ? _colorAdjustments : null;
    }

    private void OnEnable()
    {
        Timer.OnTimeReached += ApplyFade;
    }
    
    private void OnDisable()
    {
        Timer.OnTimeReached -= ApplyFade;
    }

    private void ApplyFade()
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }
        _fadeCoroutine = StartCoroutine(FadeToBlackCoroutine(fadeDuration));
    }
    
    private Coroutine _fadeCoroutine;
    
    private IEnumerator FadeToBlackCoroutine(float duration)
    {
        _colorAdjustments.colorFilter.overrideState = true;

        Color start = _colorAdjustments.colorFilter.value;
        while (_timer < duration)
        {
            _timer += Time.deltaTime;
            float t = Mathf.Clamp01(_timer / duration);
            _colorAdjustments.colorFilter.value = Color.Lerp(start, Color.black, t);
            yield return null;
        }
        _colorAdjustments.colorFilter.value = Color.black;
        SceneManager.LoadScene("Credits");
    }
    
}
