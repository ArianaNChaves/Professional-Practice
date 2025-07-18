using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficerState : EnemyState
{

    public PoliceOfficerState()
    {
        EnemyStates.Add(STATE.IDLE,   new PoliceIdle());
        EnemyStates.Add(STATE.CHASE,  new PoliceChase());
        EnemyStates.Add(STATE.DEATH,  new PoliceDeath());
        EnemyStates.Add(STATE.ATTACK, new PoliceAttack());

        // Estado inicial
        state = STATE.IDLE;
        NextEnemyState = EnemyStates[STATE.IDLE];
        stage = EVENT.ENTER;
    }
    
}
