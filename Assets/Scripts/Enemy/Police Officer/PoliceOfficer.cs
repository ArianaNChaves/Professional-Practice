using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PoliceOfficer : EnemyTemplate, IDamageable, IPooleable
{
    public static event Action OnEnemyDeath;
    public static event Action<PoliceOfficer> OnSpawn;
    
    private GameObject _currentTarget;
    private List<GameObject> _targetsList;
    
    //todo ------------Borrar------------------Borrar-----------------------Borrar--------------------------- Borrar
    [SerializeField] private TextMeshProUGUI myState;
    [SerializeField] private TextMeshProUGUI myStates;
    
    protected override void Awake()
    {
        base.Awake();
        EnemyRigidbody = GetComponent<Rigidbody>();
        EnemyCollider = GetComponent<Collider>();
        _targetsList = new List<GameObject>();
        
        States = new Dictionary<EnemyState.STATE, EnemyState>
        {
            { EnemyState.STATE.IDLE, new PoliceIdle() },
            { EnemyState.STATE.CHASE, new PoliceChase() },
            { EnemyState.STATE.ATTACK, new PoliceAttack() },
            { EnemyState.STATE.DEATH, new PoliceDeath() },
        };
        
        CurrentState = States[EnemyState.STATE.IDLE];
    }

    protected void Update()
    {
        CurrentState = CurrentState.Process(); //todo Esto va a aca o en el padre?
        
        
        
        //todo ------------Borrar------------------Borrar-----------------------Borrar--------------------------- Borrar
        DebugText();
        if (Input.GetKeyDown(KeyCode.O))
        {
            CurrentState.ChangeState(States, EnemyState.STATE.DEATH);
            // _currentState = _currentState.Process(); 
    
            Debug.Log($"→ Estado actual: {CurrentState.GetState()}");

            var posibles = string.Join(", ", GetAvailableStates());
            Debug.Log($"→ Estados disponibles: {posibles}");
            
            Debug.Log($"→ Estado actual: {CurrentState.GetState()}");
            // _currentState.ChangeState(EnemyState.STATE.CHASE);
        }
    }

    private void DebugText()
    {
        myState.text = CurrentState.GetState().ToString();
        
        myStates.text = string.Join("\n", GetAvailableStates());
    }
    
    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }

    public void ReturnObjectToPool()
    {
        throw new NotImplementedException();
    }
}
