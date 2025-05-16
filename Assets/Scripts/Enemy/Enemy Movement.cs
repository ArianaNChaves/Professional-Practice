using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum State
    {
        Idle,
        Moving,
        Attacking
    }
    
    private State _currentState;
    
    private void Start()
    {
        _currentState = State.Idle;   
    }
}
