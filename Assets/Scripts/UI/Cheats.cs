using System;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public static event Action OnGodMode;
    [SerializeField] private GameObject[] cheatsOn;
    [SerializeField] private GameObject[] cheatsOff;
    private bool _cheatsEnabled = false;
    private void OnEnable()
    {
        PlayerInputs.OnCheats += CheatsToggle;
    }
    private void OnDisable()
    {
        PlayerInputs.OnCheats -= CheatsToggle;
    }

    private void CheatsToggle()
    {
        _cheatsEnabled = !_cheatsEnabled;
        Debug.Log(_cheatsEnabled);

        for (int i = 0; i < cheatsOn.Length; i++)
        {
            cheatsOn[i].SetActive(_cheatsEnabled);
            cheatsOff[i].SetActive(!_cheatsEnabled);
        }
        OnGodMode?.Invoke();
    }
    
}
