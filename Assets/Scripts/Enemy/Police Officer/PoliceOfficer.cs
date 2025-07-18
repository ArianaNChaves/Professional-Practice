using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoliceOfficer : MonoBehaviour
{
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private TextMeshProUGUI myState;
    [SerializeField] private TextMeshProUGUI myStates;
    private EnemyState _currentState;
    
    private void Awake()
    {
        _currentState = new PoliceOfficerState();
    }
    private void Update()
    {
        _currentState = _currentState.Process();
        DebugText();
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            _currentState.ChangeState(EnemyState.STATE.ATTACK);
            // _currentState = _currentState.Process(); //todo esta va en un update? o una corrutina? el asunto que se tiene que procesar cada frame

            Debug.Log($"→ Estado actual: {_currentState.GetState()}");

            var posibles = string.Join(", ", _currentState.GetAvailableStates());
            Debug.Log($"→ Estados disponibles: {posibles}");
            
            _currentState.ChangeState(EnemyState.STATE.RECOVER);
            Debug.Log($"→ Estado actual: {_currentState.GetState()}");
            // _currentState.ChangeState(EnemyState.STATE.CHASE);
        }
    }

    private void DebugText()
    {
        myState.text = _currentState.GetState().ToString();
        
        myStates.text = string.Join("\n", _currentState.GetAvailableStates());
    }
}
