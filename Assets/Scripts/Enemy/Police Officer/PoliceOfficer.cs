using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// using static EnemyState;

public class PoliceOfficer : MonoBehaviour
{
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private TextMeshProUGUI myState;
    [SerializeField] private TextMeshProUGUI myStates;
    private EnemyState _currentState;
    private Dictionary<EnemyState.STATE, EnemyState> _states;
    
    private void Awake()
    {
        // PoliceOfficer manages its own states
        _states = new Dictionary<EnemyState.STATE, EnemyState>
        {
            { EnemyState.STATE.IDLE, new PoliceIdle() },
            { EnemyState.STATE.CHASE, new PoliceChase() },
            { EnemyState.STATE.ATTACK, new PoliceAttack() },
            { EnemyState.STATE.DEATH, new PoliceDeath() },
        };
        
        _currentState = _states[EnemyState.STATE.IDLE];
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

            var posibles = string.Join(", ", GetAvailableStates());
            Debug.Log($"→ Estados disponibles: {posibles}");
            
            Debug.Log($"→ Estado actual: {_currentState.GetState()}");
            // _currentState.ChangeState(EnemyState.STATE.CHASE);
        }
    }

    private void DebugText()
    {
        myState.text = _currentState.GetState().ToString();
        
        myStates.text = string.Join("\n", GetAvailableStates());
    }
    public IEnumerable<EnemyState.STATE> GetAvailableStates() => _states.Keys;

}
