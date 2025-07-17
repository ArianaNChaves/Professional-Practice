using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficer : MonoBehaviour
{
    private PoliceOfficerState _policeOfficerState;
    
    private void Awake()
    {
        _policeOfficerState = new PoliceOfficerState();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            _policeOfficerState = new PoliceAttack();
            _policeOfficerState.Process();
            Debug.Log(_policeOfficerState.GetState());
            _policeOfficerState.ChangeState(EnemyState.STATE.IDLE);
            Debug.Log(_policeOfficerState.GetState());
        }
    }
}
